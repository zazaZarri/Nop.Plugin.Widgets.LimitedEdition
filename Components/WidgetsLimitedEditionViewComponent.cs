using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Plugin.Widgets.LimitedEdition.Models;
using Nop.Plugin.Widgets.LimitedEdition.Services;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
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
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWebHelper _webHelper;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWorkContext _workContext;
        private readonly IRepository<CustomerTable> _customerActionRepository;

        public WidgetsLimitedEditionViewComponent(
            ILimitedTimeProductService limitedTimeProductService,
            IProductService productService,
            ISettingService settingService,
            IStoreContext storeContext,
            IUrlRecordService urlRecordService,
            IWebHelper webHelper,
            IShoppingCartService shoppingCartService,
            IWorkContext workContext,
            IRepository<CustomerTable> customerActionRepository)
        {
            _limitedTimeProductService = limitedTimeProductService;
            _productService = productService;
            _settingService = settingService;
            _storeContext = storeContext;
            _urlRecordService = urlRecordService;
            _webHelper = webHelper;
            _shoppingCartService = shoppingCartService;
            _workContext = workContext;
            _customerActionRepository = customerActionRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData = null)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);

            var style = StyleSettingsModel.FromSettings(settings);
            var message = string.IsNullOrEmpty(settings.CustomMessage)
                ? "Prodotto a tempo limitato!"
                : settings.CustomMessage;

            var productId = GetProductById(additionalData);
            if (productId > 0)
                return await InvokeProductPageAsync(productId, message, style);

            if (widgetZone == PublicWidgetZones.HomepageTop || widgetZone == PublicWidgetZones.HomepageBeforeProducts)
                return await InvokeHomepageAsync(message, style);

            if (widgetZone == PublicWidgetZones.OrderSummaryContentBefore)
                return await InvokeCartPopupAsync(message, style);

            return Content("");
        }

        private async Task<IViewComponentResult> InvokeCartPopupAsync(string message, StyleSettingsModel style)
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var cart = await _shoppingCartService.GetShoppingCartAsync(customer, ShoppingCartType.ShoppingCart);
            var all = await _limitedTimeProductService.GetAllPagedAsync(0, 20);
            var offers = new List<PublicInfoModel>();
            var isCustomerEnabled = false;

            var customerAction = await _customerActionRepository.Table
                .FirstOrDefaultAsync(x => x.CustomerId == customer.Id);
            if (customerAction != null)
                isCustomerEnabled = customerAction.IsEnabled;

            foreach (var item in all)
            {
                if (!item.IsActive) continue;
                if (item.StartDateUtc > DateTime.UtcNow) continue;
                if (item.EndDateUtc < DateTime.UtcNow) continue;

                var product = await _productService.GetProductByIdAsync(item.ProductId);
                if (product == null || !product.Published) continue;

                var seName = await _urlRecordService.GetSeNameAsync(product);
                var productUrl = _webHelper.GetStoreLocation().TrimEnd('/') + "/" + seName;

                offers.Add(new PublicInfoModel
                {
                    ProductId = item.ProductId,
                    EndDateUtc = item.EndDateUtc,
                    CustomMessage = message,
                    ProductName = product.Name,
                    ProductUrl = productUrl,
                    Style = style
                });
            }

            var model = new HomepageOffersModel
            {
                IsProductPage = false,
                Offers = offers,
                Style = style
            };

            if (isCustomerEnabled && cart.Count > 0)
            {
                return View("~/Plugins/Widgets.LimitedEdition/Views/Shared/Components/WidgetsLimitedEdition/CartPopup.cshtml", model);
            }

            return Content("");
        }

        private async Task<IViewComponentResult> InvokeProductPageAsync(int productId, string message, StyleSettingsModel style)
        {
            var limited = await _limitedTimeProductService.GetActiveByProductIdAsync(productId);
            if (limited == null)
                return Content("");

            var model = new HomepageOffersModel
            {
                IsProductPage = true,
                Style = style
            };
            model.Offers.Add(new PublicInfoModel
            {
                ProductId = productId,
                EndDateUtc = limited.EndDateUtc,
                CustomMessage = message,
                Style = style
            });

            return View("~/Plugins/Widgets.LimitedEdition/Views/Shared/Components/WidgetsLimitedEdition/LimitedEditionView.cshtml", model);
        }

        private async Task<IViewComponentResult> InvokeHomepageAsync(string message, StyleSettingsModel style)
        {
            var all = await _limitedTimeProductService.GetAllPagedAsync(0, 20);
            var model = new HomepageOffersModel
            {
                IsProductPage = false,
                Style = style
            };

            foreach (var item in all)
            {
                if (!item.IsActive) continue;
                if (item.StartDateUtc > DateTime.UtcNow) continue;
                if (item.EndDateUtc < DateTime.UtcNow) continue;

                var product = await _productService.GetProductByIdAsync(item.ProductId);
                if (product == null || !product.Published) continue;

                var seName = await _urlRecordService.GetSeNameAsync(product);
                var productUrl = _webHelper.GetStoreLocation().TrimEnd('/') + "/" + seName;

                model.Offers.Add(new PublicInfoModel
                {
                    ProductId = item.ProductId,
                    EndDateUtc = item.EndDateUtc,
                    CustomMessage = message,
                    ProductName = product.Name,
                    ProductUrl = productUrl,
                    Style = style
                });
            }

            if (!model.Offers.Any())
                return Content("");

            return View("~/Plugins/Widgets.LimitedEdition/Views/Shared/Components/WidgetsLimitedEdition/LimitedEditionView.cshtml", model);
        }

        private static int GetProductById(object additionalData)
        {
            switch (additionalData)
            {
                case int id:
                    return id;
                case ProductDetailsModel pdm:
                    return pdm.Id;
                case null:
                    return 0;
                default:
                    var prop = additionalData.GetType().GetProperty("Id")
                               ?? additionalData.GetType().GetProperty("ProductId");
                    return prop?.GetValue(additionalData) is int intVal ? intVal : 0;
            }
        }
    }
}
