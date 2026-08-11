using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Plugin.Widgets.LimitedEdition.Models;
using Nop.Plugin.Widgets.LimitedEdition.Services;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.LimitedEdition.Controllers
{
    [Area(AreaNames.ADMIN)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class LimitedTimeController : BasePluginController
    {
        private readonly ILimitedTimeProductService _limitedTimeProductService;
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IPermissionService _permissionService;

        public LimitedTimeController(
            ILimitedTimeProductService limitedTimeProductService,
            IProductService productService,
            ISettingService settingService,
            IStoreContext storeContext,
            IPermissionService permissionService)
        {
            _limitedTimeProductService = limitedTimeProductService;
            _productService = productService;
            _settingService = settingService;
            _storeContext = storeContext;
            _permissionService = permissionService;

        }

        #region Configure

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return AccessDeniedView();

            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);

            var model = MapSettingsToModel(settings, store.Id);
            model.LimitedTimeProductSearchModel = new LimitedTimeProductSearchModel();
            model.LimitedTimeProductSearchModel.SetGridPageSize();

            return View("~/Plugins/Widgets.LimitedEdition/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return AccessDeniedView();

            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);

            // Testi
            settings.CustomMessage = model.CustomMessage;
            settings.BadgeText = model.BadgeText;
            settings.CtaText = model.CtaText;
            settings.ExpiredText = model.ExpiredText;
            settings.DaysLabel = model.DaysLabel;
            settings.HoursLabel = model.HoursLabel;
            settings.MinutesLabel = model.MinutesLabel;
            settings.SecondsLabel = model.SecondsLabel;

            // Layout
            settings.TimerLayout = (TimerLayoutType)model.TimerLayoutId;
            settings.HideProductWhenExpired = model.HideProductWhenExpired;
            settings.ShowBadge = model.ShowBadge;
            settings.ShowTitle = model.ShowTitle;
            settings.ShowMessage = model.ShowMessage;
            settings.ShowCta = model.ShowCta;
            settings.EnableGlowAnimation = model.EnableGlowAnimation;
            settings.EnableSheenAnimation = model.EnableSheenAnimation;
            settings.CardTextAlign = model.CardTextAlign;

            // Colori
            settings.AccentColor = model.AccentColor;
            settings.AccentColorLight = model.AccentColorLight;
            settings.CardBackgroundStart = model.CardBackgroundStart;
            settings.CardBackgroundMid = model.CardBackgroundMid;
            settings.CardBackgroundEnd = model.CardBackgroundEnd;
            settings.BorderColor = model.BorderColor;
            settings.TitleColor = model.TitleColor;
            settings.MessageColor = model.MessageColor;
            settings.BadgeTextColor = model.BadgeTextColor;
            settings.BadgeBackgroundStart = model.BadgeBackgroundStart;
            settings.BadgeBackgroundEnd = model.BadgeBackgroundEnd;
            settings.TimerDigitColor = model.TimerDigitColor;
            settings.TimerLabelColor = model.TimerLabelColor;
            settings.TimerBoxBackground = model.TimerBoxBackground;
            settings.TimerBoxBorderColor = model.TimerBoxBorderColor;
            settings.CtaBackgroundStart = model.CtaBackgroundStart;
            settings.CtaBackgroundEnd = model.CtaBackgroundEnd;
            settings.CtaTextColor = model.CtaTextColor;

            // Dimensioni timer
            settings.TimerDigitFontSize = model.TimerDigitFontSize;
            settings.TimerLabelFontSize = model.TimerLabelFontSize;
            settings.TimerBoxMinWidth = model.TimerBoxMinWidth;
            settings.TimerBoxBorderRadius = model.TimerBoxBorderRadius;
            settings.TimerBoxPadding = model.TimerBoxPadding;
            settings.TimerGap = model.TimerGap;

            // Card
            settings.CardBorderRadius = model.CardBorderRadius;
            settings.CardPaddingTop = model.CardPaddingTop;
            settings.CardPaddingSide = model.CardPaddingSide;
            settings.CardPaddingBottom = model.CardPaddingBottom;
            settings.CardMarginBottom = model.CardMarginBottom;
            settings.CardBorderWidth = model.CardBorderWidth;

            // Tipografia
            settings.TitleFontSize = model.TitleFontSize;
            settings.TitleFontWeight = model.TitleFontWeight;
            settings.MessageFontSize = model.MessageFontSize;
            settings.BadgeFontSize = model.BadgeFontSize;
            settings.BadgeFontWeight = model.BadgeFontWeight;
            settings.BadgeLetterSpacing = model.BadgeLetterSpacing;
            settings.BadgePaddingY = model.BadgePaddingY;
            settings.BadgePaddingX = model.BadgePaddingX;

            // CTA
            settings.CtaFontSize = model.CtaFontSize;
            settings.CtaFontWeight = model.CtaFontWeight;
            settings.CtaPaddingY = model.CtaPaddingY;
            settings.CtaPaddingX = model.CtaPaddingX;
            settings.CtaBorderRadius = model.CtaBorderRadius;


            settings.EnableCartPopup = model.EnableCartPopup;
            settings.PopupShowDelayMs = model.PopupShowDelayMs;
            settings.PopupOncePerSession = model.PopupOncePerSession;
            settings.PopupAnimationType = model.PopupAnimationType;
            settings.PopupAnimationDurationMs = model.PopupAnimationDurationMs;
            settings.PopupCloseOnOverlayClick = model.PopupCloseOnOverlayClick;
            settings.PopupCloseOnEscape = model.PopupCloseOnEscape;
            settings.PopupOverlayOpacity = model.PopupOverlayOpacity;
            settings.PopupOverlayBlurPx = model.PopupOverlayBlurPx;
            settings.PopupModalMaxWidth = model.PopupModalMaxWidth;
            settings.PopupTitle = model.PopupTitle;
            settings.PopupSubtitle = model.PopupSubtitle;
            settings.PopupContinueText = model.PopupContinueText;
            settings.PopupShowBadge = model.PopupShowBadge;
            settings.PopupShowProductList = model.PopupShowProductList;
            settings.PopupEnableGlow = model.PopupEnableGlow;
            settings.PopupEnableSheen = model.PopupEnableSheen;





            // Legacy
            settings.BackgroundColor = model.AccentColor;
            settings.TextColor = model.TitleColor;

            settings.FontFamily = model.FontFamily;

            await _settingService.SaveSettingAsync(settings, store.Id);
            await _settingService.ClearCacheAsync();

            return await Configure();
        }

        private static ConfigurationModel MapSettingsToModel(LimitedTimeSettings settings, int storeId)
        {
            return new ConfigurationModel
            {
                CustomMessage = settings.CustomMessage,
                BadgeText = settings.BadgeText,
                CtaText = settings.CtaText,
                ExpiredText = settings.ExpiredText,
                DaysLabel = settings.DaysLabel,
                HoursLabel = settings.HoursLabel,
                MinutesLabel = settings.MinutesLabel,
                SecondsLabel = settings.SecondsLabel,

                TimerLayoutId = (int)settings.TimerLayout,
                HideProductWhenExpired = settings.HideProductWhenExpired,
                ShowBadge = settings.ShowBadge,
                ShowTitle = settings.ShowTitle,
                ShowMessage = settings.ShowMessage,
                ShowCta = settings.ShowCta,
                EnableGlowAnimation = settings.EnableGlowAnimation,
                EnableSheenAnimation = settings.EnableSheenAnimation,
                CardTextAlign = settings.CardTextAlign,

                AccentColor = settings.AccentColor,
                AccentColorLight = settings.AccentColorLight,
                CardBackgroundStart = settings.CardBackgroundStart,
                CardBackgroundMid = settings.CardBackgroundMid,
                CardBackgroundEnd = settings.CardBackgroundEnd,
                BorderColor = settings.BorderColor,
                TitleColor = settings.TitleColor,
                MessageColor = settings.MessageColor,
                BadgeTextColor = settings.BadgeTextColor,
                BadgeBackgroundStart = settings.BadgeBackgroundStart,
                BadgeBackgroundEnd = settings.BadgeBackgroundEnd,
                TimerDigitColor = settings.TimerDigitColor,
                TimerLabelColor = settings.TimerLabelColor,
                TimerBoxBackground = settings.TimerBoxBackground,
                TimerBoxBorderColor = settings.TimerBoxBorderColor,
                CtaBackgroundStart = settings.CtaBackgroundStart,
                CtaBackgroundEnd = settings.CtaBackgroundEnd,
                CtaTextColor = settings.CtaTextColor,

                TimerDigitFontSize = settings.TimerDigitFontSize,
                TimerLabelFontSize = settings.TimerLabelFontSize,
                TimerBoxMinWidth = settings.TimerBoxMinWidth,
                TimerBoxBorderRadius = settings.TimerBoxBorderRadius,
                TimerBoxPadding = settings.TimerBoxPadding,
                TimerGap = settings.TimerGap,

                CardBorderRadius = settings.CardBorderRadius,
                CardPaddingTop = settings.CardPaddingTop,
                CardPaddingSide = settings.CardPaddingSide,
                CardPaddingBottom = settings.CardPaddingBottom,
                CardMarginBottom = settings.CardMarginBottom,
                CardBorderWidth = settings.CardBorderWidth,

                TitleFontSize = settings.TitleFontSize,
                TitleFontWeight = settings.TitleFontWeight,
                MessageFontSize = settings.MessageFontSize,
                BadgeFontSize = settings.BadgeFontSize,
                BadgeFontWeight = settings.BadgeFontWeight,
                BadgeLetterSpacing = settings.BadgeLetterSpacing,
                BadgePaddingY = settings.BadgePaddingY,
                BadgePaddingX = settings.BadgePaddingX,

                CtaFontSize = settings.CtaFontSize,
                CtaFontWeight = settings.CtaFontWeight,
                CtaPaddingY = settings.CtaPaddingY,
                CtaPaddingX = settings.CtaPaddingX,
                CtaBorderRadius = settings.CtaBorderRadius,

                BackgroundColor = settings.BackgroundColor,
                TextColor = settings.TextColor,
                ActiveStoreScopeConfiguration = storeId,

                EnableCartPopup = settings.EnableCartPopup,
                PopupShowDelayMs = settings.PopupShowDelayMs,
                PopupOncePerSession = settings.PopupOncePerSession,
                PopupAnimationType = settings.PopupAnimationType,
                PopupAnimationDurationMs = settings.PopupAnimationDurationMs,
                PopupCloseOnOverlayClick = settings.PopupCloseOnOverlayClick,
                PopupCloseOnEscape = settings.PopupCloseOnEscape,
                PopupOverlayOpacity = settings.PopupOverlayOpacity,
                PopupOverlayBlurPx = settings.PopupOverlayBlurPx,
                PopupModalMaxWidth = settings.PopupModalMaxWidth,
                PopupTitle = settings.PopupTitle,
                PopupSubtitle = settings.PopupSubtitle,
                PopupContinueText = settings.PopupContinueText,
                PopupShowBadge = settings.PopupShowBadge,
                PopupShowProductList = settings.PopupShowProductList,
                PopupEnableGlow = settings.PopupEnableGlow,
                PopupEnableSheen = settings.PopupEnableSheen,

                FontFamily = settings.FontFamily
            };
        }

        #endregion

        #region Products Grid

        [HttpPost]
        public async Task<IActionResult> ProductList(LimitedTimeProductSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { Data = new List<LimitedTimeProductModel>(), Total = 0 });

            var items = await _limitedTimeProductService.GetAllPagedAsync(
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            var productModels = new List<LimitedTimeProductModel>();

            foreach (var item in items)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);

                productModels.Add(new LimitedTimeProductModel
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = product?.Name ?? $"[Prodotto eliminato - ID {item.ProductId}]",
                    StartDateUtc = item.StartDateUtc,
                    EndDateUtc = item.EndDateUtc,
                    IsActive = item.IsActive
                });
            }

            return Json(new
            {
                Data = productModels,
                Total = items.TotalCount
            });
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(LimitedTimeProductModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { Result = false, Error = "Access denied" });

            if (model.Id > 0)
            {
                var entity = await _limitedTimeProductService.GetByIdAsync(model.Id);
                if (entity == null)
                    return Json(new { Result = false, Error = "Record non trovato" });

                entity.ProductId = model.ProductId;
                entity.StartDateUtc = model.StartDateUtc ?? DateTime.UtcNow;
                entity.EndDateUtc = model.EndDateUtc ?? DateTime.UtcNow.AddDays(7);
                entity.IsActive = model.IsActive;

                await _limitedTimeProductService.UpdateAsync(entity);
            }
            else
            {
                var entity = new LimitedTimeProduct
                {
                    ProductId = model.ProductId,
                    StartDateUtc = model.StartDateUtc ?? DateTime.UtcNow,
                    EndDateUtc = model.EndDateUtc ?? DateTime.UtcNow.AddDays(7),
                    IsActive = model.IsActive
                };

                await _limitedTimeProductService.InsertAsync(entity);
            }

            return Json(new { Result = true });
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { Result = false, Error = "Access denied" });

            var entity = await _limitedTimeProductService.GetByIdAsync(id);
            if (entity == null)
                return Json(new { Result = false, Error = "Record non trovato" });

            await _limitedTimeProductService.DeleteAsync(entity);

            return Json(new { Result = true });
        }

        [HttpGet]
        public async Task<IActionResult> ProductSearchAutocomplete(string term)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new List<object>());

            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            term = term.Trim();
            var result = new List<object>();

            // Cerca per ID se è un numero
            if (int.TryParse(term, out int productId))
            {
                var product = await _productService.GetProductByIdAsync(productId);
                if (product != null)
                {
                    result.Add(new
                    {
                        id = product.Id,
                        label = $"{product.Name} [ID: {product.Id}]",
                        value = product.Name
                    });
                    return Json(result);
                }
            }

            var products = await _productService.SearchProductsAsync(
                pageIndex: 0,
                pageSize: 25,
                keywords: term,
                searchSku: true,
                searchManufacturerPartNumber: true,
                showHidden: true,
                visibleIndividuallyOnly: false
            );

            foreach (var p in products)
            {
                result.Add(new
                {
                    id = p.Id,
                    label = $"{p.Name} [ID: {p.Id}]",
                    value = p.Name
                });
            }

            return Json(result);
        }

        #endregion
    }
}
