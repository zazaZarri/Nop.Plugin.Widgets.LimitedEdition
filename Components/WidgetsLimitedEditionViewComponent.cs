using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Plugin.Widgets.LimitedEdition.Models;
using Nop.Plugin.Widgets.LimitedEdition.Services;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.LimitedEdition.Components
{
    public class WidgetsLimitedEditionViewComponent : NopViewComponent
    {
        private readonly ILimitedTimeProductService _limitedTimeProductService;
        private readonly ILimitedEditionFeatureService _featureService;
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWebHelper _webHelper;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWorkContext _workContext;
        private readonly IRepository<CustomerTable> _customerActionRepository;
        private readonly IPictureService _pictureService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WidgetsLimitedEditionViewComponent(
            ILimitedTimeProductService limitedTimeProductService,
            ILimitedEditionFeatureService featureService,
            IProductService productService,
            ISettingService settingService,
            IStoreContext storeContext,
            IUrlRecordService urlRecordService,
            IWebHelper webHelper,
            IShoppingCartService shoppingCartService,
            IWorkContext workContext,
            IRepository<CustomerTable> customerActionRepository,
            IPictureService pictureService,
            IHttpContextAccessor httpContextAccessor)
        {
            _limitedTimeProductService = limitedTimeProductService;
            _featureService = featureService;
            _productService = productService;
            _settingService = settingService;
            _storeContext = storeContext;
            _urlRecordService = urlRecordService;
            _webHelper = webHelper;
            _shoppingCartService = shoppingCartService;
            _workContext = workContext;
            _customerActionRepository = customerActionRepository;
            _pictureService = pictureService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData = null)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);

            var sessionId = _httpContextAccessor.HttpContext?.Session?.Id
                            ?? _httpContextAccessor.HttpContext?.Request.Cookies[".Nop.Customer"]
                            ?? Guid.NewGuid().ToString("N");

            // Template id (A/B opzionale). Niente override nascosto: ciò che salvi in admin è ciò che vedi.
            var cardId = _featureService.ResolveCardTemplateId(settings, sessionId);
            var popupId = settings.SelectedPopupTemplateId;
            if (cardId < 0) cardId = settings.SelectedCardTemplateId;
            if (popupId < 0) popupId = 0;

            // Stile = settings flat (ultimo salvataggio admin) — fonte unica di verità per la view pubblica
            var cardStyle = StyleSettingsModel.FromLimitedTimeSettings(settings);
            cardStyle.CardTemplateId = cardId;
            cardStyle.PopupTemplateId = popupId;

            var popupStyle = StyleSettingsModel.FromLimitedTimeSettings(settings);
            popupStyle.CardTemplateId = cardId;
            popupStyle.PopupTemplateId = popupId;

            var message = string.IsNullOrEmpty(settings.CustomMessage)
                ? "Prodotto a tempo limitato!"
                : settings.CustomMessage;

            var baseUrl = _webHelper.GetStoreLocation().TrimEnd('/');

            var productId = GetProductById(additionalData);
            if (productId > 0)
                return await InvokeProductPageAsync(productId, message, cardStyle, cardId, settings, widgetZone, baseUrl);

            if (widgetZone == PublicWidgetZones.HomepageTop || widgetZone == PublicWidgetZones.HomepageBeforeProducts)
                return await InvokeHomepageAsync(message, cardStyle, cardId, settings, widgetZone, baseUrl);

            if (widgetZone == PublicWidgetZones.OrderSummaryContentBefore)
                return await InvokeCartPopupAsync(message, popupStyle, popupId, settings, baseUrl);

            return Content("");
        }

        private HomepageOffersModel BuildShell(StyleSettingsModel style, int cardId, int popupId, LimitedTimeSettings settings, string zone, string baseUrl, bool isProduct)
        {
            return new HomepageOffersModel
            {
                IsProductPage = isProduct,
                Style = style,
                CardTemplateId = cardId,
                PopupTemplateId = popupId,
                EnableSocialProof = settings.EnableSocialProof,
                SocialProofIntervalSeconds = settings.SocialProofIntervalSeconds > 0 ? settings.SocialProofIntervalSeconds : 12,
                EnableLastHourSound = settings.EnableLastHourSound,
                EnableServerCountdown = settings.EnableServerCountdown,
                CompactLayout = isProduct && settings.CompactOnProductPage,
                WidgetZone = zone,
                ServerCountdownUrl = baseUrl + "/LimitedEditionPublic/Countdown",
                SocialProofFeedUrl = baseUrl + "/LimitedEditionPublic/SocialProofFeed",
                UseProductImageAsBackground = settings.UseProductImageAsBackground
            };
        }

        private async Task<PublicInfoModel> BuildOfferAsync(LimitedTimeProduct item, string message, StyleSettingsModel style, LimitedTimeSettings settings, string baseUrl)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            if (product == null || !product.Published) return null;

            var seName = await _urlRecordService.GetSeNameAsync(product);
            var productUrl = baseUrl + "/" + seName;

            string imageUrl = null;
            try
            {
                var pictures = await _pictureService.GetPicturesByProductIdAsync(product.Id, 1);
                if (pictures != null && pictures.Count > 0)
                    imageUrl = await _pictureService.GetPictureUrlAsync(pictures[0].Id, 800);
            }
            catch { /* picture optional */ }

            var offer = new PublicInfoModel
            {
                ProductId = item.ProductId,
                EndDateUtc = item.EndDateUtc,
                StartDateUtc = item.StartDateUtc,
                CustomMessage = message,
                ProductName = product.Name,
                ProductUrl = productUrl,
                ProductImageUrl = imageUrl,
                Style = style
            };
            return _featureService.EnrichOffer(offer, item, settings);
        }

        private async Task<IViewComponentResult> InvokeCartPopupAsync(string message, StyleSettingsModel style, int popupTemplateId, LimitedTimeSettings settings, string baseUrl)
        {
            if (!style.EnableCartPopup)
                return Content("");

            var customer = await _workContext.GetCurrentCustomerAsync();
            var cart = await _shoppingCartService.GetShoppingCartAsync(customer, ShoppingCartType.ShoppingCart);
            var all = await _limitedTimeProductService.GetAllPagedAsync(0, 20);
            var offers = new List<PublicInfoModel>();

            var customerAction = await _customerActionRepository.Table
                .FirstOrDefaultAsync(x => x.CustomerId == customer.Id);
            var isCustomerEnabled = customerAction?.IsEnabled == true;

            var now = DateTime.UtcNow;
            foreach (var item in all)
            {
                if (!item.IsActive || item.StartDateUtc > now || item.EndDateUtc < now) continue;
                var offer = await BuildOfferAsync(item, message, style, settings, baseUrl);
                if (offer != null) offers.Add(offer);
            }

            var model = BuildShell(style, style.CardTemplateId, popupTemplateId, settings, PublicWidgetZones.OrderSummaryContentBefore, baseUrl, false);
            model.Offers = offers;

            if (isCustomerEnabled && cart.Count > 0 && offers.Any())
                return View("~/Plugins/Widgets.LimitedEdition/Views/Shared/Components/WidgetsLimitedEdition/CartPopup.cshtml", model);

            return Content("");
        }

        private async Task<IViewComponentResult> InvokeProductPageAsync(int productId, string message, StyleSettingsModel style, int cardTemplateId, LimitedTimeSettings settings, string zone, string baseUrl)
        {
            var limited = await _limitedTimeProductService.GetActiveByProductIdAsync(productId);
            if (limited == null)
                return Content("");

            var offer = await BuildOfferAsync(limited, message, style, settings, baseUrl);
            if (offer == null) return Content("");

            var model = BuildShell(style, cardTemplateId, settings.SelectedPopupTemplateId, settings, zone, baseUrl, true);
            model.Offers.Add(offer);

            return View("~/Plugins/Widgets.LimitedEdition/Views/Shared/Components/WidgetsLimitedEdition/LimitedEditionView.cshtml", model);
        }

        private async Task<IViewComponentResult> InvokeHomepageAsync(string message, StyleSettingsModel style, int cardTemplateId, LimitedTimeSettings settings, string zone, string baseUrl)
        {
            var all = await _limitedTimeProductService.GetAllPagedAsync(0, 20);
            var model = BuildShell(style, cardTemplateId, settings.SelectedPopupTemplateId, settings, zone, baseUrl, false);
            var now = DateTime.UtcNow;

            foreach (var item in all)
            {
                if (!item.IsActive || item.StartDateUtc > now || item.EndDateUtc < now) continue;
                if (settings.HideProductWhenExpired && item.EndDateUtc < now) continue;
                var offer = await BuildOfferAsync(item, message, style, settings, baseUrl);
                if (offer != null) model.Offers.Add(offer);
            }

            if (!model.Offers.Any())
                return Content("");

            return View("~/Plugins/Widgets.LimitedEdition/Views/Shared/Components/WidgetsLimitedEdition/LimitedEditionView.cshtml", model);
        }

        private static int GetProductById(object additionalData)
        {
            switch (additionalData)
            {
                case int id: return id;
                case ProductDetailsModel pdm: return pdm.Id;
                case null: return 0;
                default:
                    var prop = additionalData.GetType().GetProperty("Id")
                               ?? additionalData.GetType().GetProperty("ProductId");
                    return prop?.GetValue(additionalData) is int intVal ? intVal : 0;
            }
        }
    }
}
