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
using Nop.Services.Messages;
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
        private readonly INotificationService _notificationService;

        public LimitedTimeController(
            ILimitedTimeProductService limitedTimeProductService,
            IProductService productService,
            ISettingService settingService,
            IStoreContext storeContext,
            IPermissionService permissionService,
            INotificationService notificationService)
        {
            _limitedTimeProductService = limitedTimeProductService;
            _productService = productService;
            _settingService = settingService;
            _storeContext = storeContext;
            _permissionService = permissionService;
            _notificationService = notificationService;
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

            try
            {
                var store = await _storeContext.GetCurrentStoreAsync();
                var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id)
                               ?? new LimitedTimeSettings();

                // Template selezionati
                settings.SelectedCardTemplateId = model.SelectedCardTemplateId;
                settings.SelectedPopupTemplateId = model.SelectedPopupTemplateId;

                // Testi
                settings.CustomMessage = model.CustomMessage;
                settings.BadgeText = model.BadgeText;
                settings.CtaText = model.CtaText;
                settings.ExpiredText = model.ExpiredText;
                settings.DaysLabel = model.DaysLabel;
                settings.HoursLabel = model.HoursLabel;
                settings.MinutesLabel = model.MinutesLabel;
                settings.SecondsLabel = model.SecondsLabel;

                // Layout / visibilità
                settings.TimerLayout = (TimerLayoutType)model.TimerLayoutId;
                settings.HideProductWhenExpired = model.HideProductWhenExpired;
                settings.ShowBadge = model.ShowBadge;
                settings.ShowTitle = model.ShowTitle;
                settings.ShowMessage = model.ShowMessage;
                settings.ShowCta = model.ShowCta;
                settings.EnableGlowAnimation = model.EnableGlowAnimation;
                settings.EnableSheenAnimation = model.EnableSheenAnimation;
                settings.CardTextAlign = model.CardTextAlign;
                settings.FontFamily = model.FontFamily;

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

                // Dimensioni
                settings.TimerDigitFontSize = model.TimerDigitFontSize;
                settings.TimerLabelFontSize = model.TimerLabelFontSize;
                settings.TimerBoxMinWidth = model.TimerBoxMinWidth;
                settings.TimerBoxBorderRadius = model.TimerBoxBorderRadius;
                settings.TimerBoxPadding = model.TimerBoxPadding;
                settings.TimerGap = model.TimerGap;
                settings.CardBorderRadius = model.CardBorderRadius;
                settings.CardPaddingTop = model.CardPaddingTop;
                settings.CardPaddingSide = model.CardPaddingSide;
                settings.CardPaddingBottom = model.CardPaddingBottom;
                settings.CardMarginBottom = model.CardMarginBottom;
                settings.CardBorderWidth = model.CardBorderWidth;
                settings.TitleFontSize = model.TitleFontSize;
                settings.TitleFontWeight = model.TitleFontWeight;
                settings.MessageFontSize = model.MessageFontSize;
                settings.BadgeFontSize = model.BadgeFontSize;
                settings.BadgeFontWeight = model.BadgeFontWeight;
                settings.BadgeLetterSpacing = model.BadgeLetterSpacing;
                settings.BadgePaddingY = model.BadgePaddingY;
                settings.BadgePaddingX = model.BadgePaddingX;
                settings.CtaFontSize = model.CtaFontSize;
                settings.CtaFontWeight = model.CtaFontWeight;
                settings.CtaPaddingY = model.CtaPaddingY;
                settings.CtaPaddingX = model.CtaPaddingX;
                settings.CtaBorderRadius = model.CtaBorderRadius;
                settings.BackgroundColor = model.AccentColor;
                settings.TextColor = model.TitleColor;

                // Popup
                settings.EnableCartPopup = model.EnableCartPopup;
                settings.PopupShowDelayMs = model.PopupShowDelayMs;
                settings.PopupOncePerSession = model.PopupOncePerSession;
                settings.PopupCloseOnOverlayClick = model.PopupCloseOnOverlayClick;
                settings.PopupCloseOnEscape = model.PopupCloseOnEscape;
                settings.PopupAnimationType = model.PopupAnimationType;
                settings.PopupAnimationDurationMs = model.PopupAnimationDurationMs;
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

                // Feature flags
                settings.EnableSocialProof = model.EnableSocialProof;
                settings.SocialProofIntervalSeconds = model.SocialProofIntervalSeconds;
                settings.SocialProofIncludeSimulated = model.SocialProofIncludeSimulated;
                settings.EnableDynamicBadges = model.EnableDynamicBadges;
                settings.DynamicBadgeUrgentHours = model.DynamicBadgeUrgentHours;
                settings.DynamicBadgeLowStockPercent = model.DynamicBadgeLowStockPercent;
                settings.EnableLastHourSound = model.EnableLastHourSound;
                settings.EnableAbTest = model.EnableAbTest;
                settings.AbTestTemplateIds = model.AbTestTemplateIds;
                settings.EnableExpiryReminders = model.EnableExpiryReminders;
                settings.ReminderHoursBeforeExpiry = model.ReminderHoursBeforeExpiry;
                settings.CompactOnProductPage = model.CompactOnProductPage;
                settings.PreferStoryOnHomepageTop = model.PreferStoryOnHomepageTop;
                settings.UseProductImageAsBackground = model.UseProductImageAsBackground;
                settings.GlobalBlockPurchaseWhenExpired = model.GlobalBlockPurchaseWhenExpired;
                settings.DefaultShowProgressBar = model.DefaultShowProgressBar;
                settings.DefaultProgressBarMode = model.DefaultProgressBarMode;
                settings.EnableServerCountdown = model.EnableServerCountdown;

                // Template JSON (best-effort, non blocca il salvataggio)
                try
                {
                    var cardDict = StyleBag.EnsureCardTemplates(settings.CardTemplatesJson);
                    cardDict[model.SelectedCardTemplateId] = MapModelToStyleBag(model, isPopup: false);
                    settings.CardTemplatesJson = StyleBag.SerializeDict(cardDict);

                    var popupDict = StyleBag.EnsurePopupTemplates(settings.PopupTemplatesJson);
                    popupDict[model.SelectedPopupTemplateId] = MapModelToStyleBag(model, isPopup: true);
                    settings.PopupTemplatesJson = StyleBag.SerializeDict(popupDict);
                }
                catch
                {
                    // ignora errori JSON template
                }

                await _settingService.SaveSettingAsync<LimitedTimeSettings>(settings, 0);
                await _settingService.SaveSettingAsync<LimitedTimeSettings>(settings, store.Id);
                await _settingService.ClearCacheAsync();

                _notificationService.SuccessNotification("Impostazioni Limited Edition salvate correttamente.");
            }
            catch (System.Exception ex)
            {
                _notificationService.ErrorNotification("Errore salvataggio: " + ex.Message);
            }

            return await Configure();
        }


        /// <summary>
        /// Salvataggio AJAX esplicito (feedback JSON garantito).
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SaveSettingsAjax(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { success = false, message = "Accesso negato" });

            try
            {
                if (model == null)
                    return Json(new { success = false, message = "Model nullo" });

                var store = await _storeContext.GetCurrentStoreAsync();
                var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id)
                               ?? new LimitedTimeSettings();

                // ===== Template =====
                settings.SelectedCardTemplateId = model.SelectedCardTemplateId;
                settings.SelectedPopupTemplateId = model.SelectedPopupTemplateId;

                // ===== Testi card =====
                settings.CustomMessage = model.CustomMessage;
                settings.BadgeText = model.BadgeText;
                settings.CtaText = model.CtaText;
                settings.ExpiredText = model.ExpiredText;
                settings.DaysLabel = model.DaysLabel;
                settings.HoursLabel = model.HoursLabel;
                settings.MinutesLabel = model.MinutesLabel;
                settings.SecondsLabel = model.SecondsLabel;

                // ===== Layout / visibilità =====
                settings.TimerLayout = (TimerLayoutType)model.TimerLayoutId;
                settings.HideProductWhenExpired = model.HideProductWhenExpired;
                settings.ShowBadge = model.ShowBadge;
                settings.ShowTitle = model.ShowTitle;
                settings.ShowMessage = model.ShowMessage;
                settings.ShowCta = model.ShowCta;
                settings.EnableGlowAnimation = model.EnableGlowAnimation;
                settings.EnableSheenAnimation = model.EnableSheenAnimation;
                settings.CardTextAlign = model.CardTextAlign;
                settings.FontFamily = model.FontFamily;

                // ===== Colori =====
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
                settings.BackgroundColor = model.AccentColor;
                settings.TextColor = model.TitleColor;

                // ===== Dimensioni / tipografia =====
                settings.TimerDigitFontSize = model.TimerDigitFontSize;
                settings.TimerLabelFontSize = model.TimerLabelFontSize;
                settings.TimerBoxMinWidth = model.TimerBoxMinWidth;
                settings.TimerBoxBorderRadius = model.TimerBoxBorderRadius;
                settings.TimerBoxPadding = model.TimerBoxPadding;
                settings.TimerGap = model.TimerGap;
                settings.CardBorderRadius = model.CardBorderRadius;
                settings.CardPaddingTop = model.CardPaddingTop;
                settings.CardPaddingSide = model.CardPaddingSide;
                settings.CardPaddingBottom = model.CardPaddingBottom;
                settings.CardMarginBottom = model.CardMarginBottom;
                settings.CardBorderWidth = model.CardBorderWidth;
                settings.TitleFontSize = model.TitleFontSize;
                settings.TitleFontWeight = model.TitleFontWeight;
                settings.MessageFontSize = model.MessageFontSize;
                settings.BadgeFontSize = model.BadgeFontSize;
                settings.BadgeFontWeight = model.BadgeFontWeight;
                settings.BadgeLetterSpacing = model.BadgeLetterSpacing;
                settings.BadgePaddingY = model.BadgePaddingY;
                settings.BadgePaddingX = model.BadgePaddingX;
                settings.CtaFontSize = model.CtaFontSize;
                settings.CtaFontWeight = model.CtaFontWeight;
                settings.CtaPaddingY = model.CtaPaddingY;
                settings.CtaPaddingX = model.CtaPaddingX;
                settings.CtaBorderRadius = model.CtaBorderRadius;

                // ===== Popup =====
                settings.EnableCartPopup = model.EnableCartPopup;
                settings.PopupShowDelayMs = model.PopupShowDelayMs;
                settings.PopupOncePerSession = model.PopupOncePerSession;
                settings.PopupCloseOnOverlayClick = model.PopupCloseOnOverlayClick;
                settings.PopupCloseOnEscape = model.PopupCloseOnEscape;
                settings.PopupAnimationType = model.PopupAnimationType;
                settings.PopupAnimationDurationMs = model.PopupAnimationDurationMs;
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

                // ===== Feature flags =====
                settings.EnableSocialProof = model.EnableSocialProof;
                settings.SocialProofIntervalSeconds = model.SocialProofIntervalSeconds;
                settings.SocialProofIncludeSimulated = model.SocialProofIncludeSimulated;
                settings.EnableDynamicBadges = model.EnableDynamicBadges;
                settings.DynamicBadgeUrgentHours = model.DynamicBadgeUrgentHours;
                settings.DynamicBadgeLowStockPercent = model.DynamicBadgeLowStockPercent;
                settings.EnableLastHourSound = model.EnableLastHourSound;
                settings.EnableAbTest = model.EnableAbTest;
                settings.AbTestTemplateIds = model.AbTestTemplateIds;
                settings.EnableExpiryReminders = model.EnableExpiryReminders;
                settings.ReminderHoursBeforeExpiry = model.ReminderHoursBeforeExpiry;
                settings.CompactOnProductPage = model.CompactOnProductPage;
                settings.PreferStoryOnHomepageTop = model.PreferStoryOnHomepageTop;
                settings.UseProductImageAsBackground = model.UseProductImageAsBackground;
                settings.GlobalBlockPurchaseWhenExpired = model.GlobalBlockPurchaseWhenExpired;
                settings.DefaultShowProgressBar = model.DefaultShowProgressBar;
                settings.DefaultProgressBarMode = model.DefaultProgressBarMode;
                settings.EnableServerCountdown = model.EnableServerCountdown;

                // ===== Template JSON (preset per template selezionato) =====
                try
                {
                    var cardDict = StyleBag.EnsureCardTemplates(settings.CardTemplatesJson);
                    cardDict[model.SelectedCardTemplateId] = MapModelToStyleBag(model, isPopup: false);
                    settings.CardTemplatesJson = StyleBag.SerializeDict(cardDict);

                    var popupDict = StyleBag.EnsurePopupTemplates(settings.PopupTemplatesJson);
                    popupDict[model.SelectedPopupTemplateId] = MapModelToStyleBag(model, isPopup: true);
                    settings.PopupTemplatesJson = StyleBag.SerializeDict(popupDict);
                }
                catch
                {
                    // non bloccare il salvataggio delle flat settings
                }

                await _settingService.SaveSettingAsync<LimitedTimeSettings>(settings, 0);
                await _settingService.SaveSettingAsync<LimitedTimeSettings>(settings, store.Id);
                await _settingService.ClearCacheAsync();

                return Json(new
                {
                    success = true,
                    message = "Tutte le impostazioni salvate.",
                    badgeText = settings.BadgeText ?? "",
                    accentColor = settings.AccentColor ?? "",
                    ctaText = settings.CtaText ?? "",
                    showBadge = settings.ShowBadge,
                    showCta = settings.ShowCta,
                    enableCartPopup = settings.EnableCartPopup,
                    defaultShowProgressBar = settings.DefaultShowProgressBar,
                    selectedCardTemplateId = settings.SelectedCardTemplateId
                });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = ex.ToString() });
            }
        }

        /// <summary>
        /// AJAX: restituisce lo StyleBag del template richiesto (per switch live in admin).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetTemplateStyle(string kind, int templateId)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { success = false });

            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);

            StyleBag bag;
            if (string.Equals(kind, "popup", StringComparison.OrdinalIgnoreCase))
            {
                var dict = StyleBag.EnsurePopupTemplates(settings.PopupTemplatesJson);
                bag = dict.TryGetValue(templateId, out var b) ? b : StyleBag.CreateDefault((PopupTemplateType)templateId);
            }
            else
            {
                var dict = StyleBag.EnsureCardTemplates(settings.CardTemplatesJson);
                bag = dict.TryGetValue(templateId, out var b) ? b : StyleBag.CreateDefault((CardTemplateType)templateId);
            }

            return Json(new { success = true, style = bag });
        }

        private static StyleBag MapModelToStyleBag(ConfigurationModel model, bool isPopup)
        {
            var bag = new StyleBag
            {
                BadgeText = model.BadgeText,
                CtaText = model.CtaText,
                ExpiredText = model.ExpiredText,
                DaysLabel = model.DaysLabel,
                HoursLabel = model.HoursLabel,
                MinutesLabel = model.MinutesLabel,
                SecondsLabel = model.SecondsLabel,
                ShowBadge = model.ShowBadge,
                ShowTitle = model.ShowTitle,
                ShowMessage = model.ShowMessage,
                ShowCta = model.ShowCta,
                EnableGlowAnimation = model.EnableGlowAnimation,
                EnableSheenAnimation = model.EnableSheenAnimation,
                TimerLayoutId = model.TimerLayoutId,
                AccentColor = model.AccentColor,
                AccentColorLight = model.AccentColorLight,
                CardBackgroundStart = model.CardBackgroundStart,
                CardBackgroundMid = model.CardBackgroundMid,
                CardBackgroundEnd = model.CardBackgroundEnd,
                BorderColor = model.BorderColor,
                TitleColor = model.TitleColor,
                MessageColor = model.MessageColor,
                BadgeTextColor = model.BadgeTextColor,
                BadgeBackgroundStart = model.BadgeBackgroundStart,
                BadgeBackgroundEnd = model.BadgeBackgroundEnd,
                TimerDigitColor = model.TimerDigitColor,
                TimerLabelColor = model.TimerLabelColor,
                TimerBoxBackground = model.TimerBoxBackground,
                TimerBoxBorderColor = model.TimerBoxBorderColor,
                CtaBackgroundStart = model.CtaBackgroundStart,
                CtaBackgroundEnd = model.CtaBackgroundEnd,
                CtaTextColor = model.CtaTextColor,
                TimerDigitFontSize = model.TimerDigitFontSize,
                TimerLabelFontSize = model.TimerLabelFontSize,
                TimerBoxMinWidth = model.TimerBoxMinWidth,
                TimerBoxBorderRadius = model.TimerBoxBorderRadius,
                TimerBoxPadding = model.TimerBoxPadding,
                TimerGap = model.TimerGap,
                CardBorderRadius = model.CardBorderRadius,
                CardPaddingTop = model.CardPaddingTop,
                CardPaddingSide = model.CardPaddingSide,
                CardPaddingBottom = model.CardPaddingBottom,
                CardMarginBottom = model.CardMarginBottom,
                CardBorderWidth = model.CardBorderWidth,
                CardTextAlign = model.CardTextAlign,
                TitleFontSize = model.TitleFontSize,
                TitleFontWeight = model.TitleFontWeight,
                MessageFontSize = model.MessageFontSize,
                BadgeFontSize = model.BadgeFontSize,
                BadgeFontWeight = model.BadgeFontWeight,
                BadgeLetterSpacing = model.BadgeLetterSpacing,
                BadgePaddingY = model.BadgePaddingY,
                BadgePaddingX = model.BadgePaddingX,
                CtaFontSize = model.CtaFontSize,
                CtaFontWeight = model.CtaFontWeight,
                CtaPaddingY = model.CtaPaddingY,
                CtaPaddingX = model.CtaPaddingX,
                CtaBorderRadius = model.CtaBorderRadius,
                FontFamily = model.FontFamily
            };

            if (isPopup)
            {
                bag.PopupOverlayOpacity = model.PopupOverlayOpacity;
                bag.PopupOverlayBlurPx = model.PopupOverlayBlurPx;
                bag.PopupModalMaxWidth = model.PopupModalMaxWidth;
                bag.PopupAnimationType = model.PopupAnimationType;
                bag.PopupAnimationDurationMs = model.PopupAnimationDurationMs;
                bag.PopupShowBadge = model.PopupShowBadge;
                bag.PopupShowProductList = model.PopupShowProductList;
                bag.PopupEnableGlow = model.PopupEnableGlow;
                bag.PopupEnableSheen = model.PopupEnableSheen;
                bag.PopupTitle = model.PopupTitle;
                bag.PopupSubtitle = model.PopupSubtitle;
                bag.PopupContinueText = model.PopupContinueText;
            }

            return bag;
        }

        private static ConfigurationModel MapSettingsToModel(LimitedTimeSettings settings, int storeId)
        {
            var cardDict = StyleBag.EnsureCardTemplates(settings.CardTemplatesJson);
            var popupDict = StyleBag.EnsurePopupTemplates(settings.PopupTemplatesJson);

            var cardId = settings.SelectedCardTemplateId;
            if (!cardDict.ContainsKey(cardId))
                cardId = 0;
            var popupId = settings.SelectedPopupTemplateId;
            if (!popupDict.ContainsKey(popupId))
                popupId = 0;

            // Form admin = settings flat (stesso contenuto della view pubblica)
            return new ConfigurationModel
            {
                SelectedCardTemplateId = cardId,
                SelectedPopupTemplateId = popupId,
                CardTemplatesJson = StyleBag.SerializeDict(cardDict),
                PopupTemplatesJson = StyleBag.SerializeDict(popupDict),

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
                FontFamily = settings.FontFamily,

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

                ActiveStoreScopeConfiguration = storeId,

                EnableCartPopup = settings.EnableCartPopup,
                PopupShowDelayMs = settings.PopupShowDelayMs,
                PopupOncePerSession = settings.PopupOncePerSession,
                PopupCloseOnOverlayClick = settings.PopupCloseOnOverlayClick,
                PopupCloseOnEscape = settings.PopupCloseOnEscape,

                // Stile popup dal template popup selezionato
                PopupAnimationType = settings.PopupAnimationType,
                PopupAnimationDurationMs = settings.PopupAnimationDurationMs,
                PopupOverlayOpacity = settings.PopupOverlayOpacity,
                PopupOverlayBlurPx = settings.PopupOverlayBlurPx,
                PopupModalMaxWidth = settings.PopupModalMaxWidth,
                PopupTitle = settings.PopupTitle,
                PopupSubtitle = settings.PopupSubtitle,
                PopupContinueText = settings.PopupContinueText,
                PopupShowBadge = settings.PopupShowBadge,
                PopupShowProductList = settings.PopupShowProductList,
                PopupEnableGlow = settings.PopupEnableGlow,
                PopupEnableSheen = settings.PopupEnableSheen
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
                    IsActive = item.IsActive,
                    InitialQuantity = item.InitialQuantity,
                    RemainingQuantity = item.RemainingQuantity,
                    SoldCount = item.SoldCount,
                    ShowRemainingStock = item.ShowRemainingStock,
                    ShowSoldCount = item.ShowSoldCount,
                    ShowProgressBar = item.ShowProgressBar,
                    ProgressBarMode = item.ProgressBarMode,
                    DiscountPercentage = item.DiscountPercentage,
                    BlockPurchaseWhenExpired = item.BlockPurchaseWhenExpired
                });
            }

            return Json(new { Data = productModels, Total = items.TotalCount });
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
                entity.InitialQuantity = model.InitialQuantity;
                entity.RemainingQuantity = model.RemainingQuantity > 0 ? model.RemainingQuantity : model.InitialQuantity;
                entity.ShowRemainingStock = model.ShowRemainingStock;
                entity.ShowSoldCount = model.ShowSoldCount;
                entity.ShowProgressBar = model.ShowProgressBar;
                entity.ProgressBarMode = model.ProgressBarMode;
                entity.DiscountPercentage = model.DiscountPercentage;
                entity.BlockPurchaseWhenExpired = model.BlockPurchaseWhenExpired;
                await _limitedTimeProductService.UpdateAsync(entity);
            }
            else
            {
                var entity = new LimitedTimeProduct
                {
                    ProductId = model.ProductId,
                    StartDateUtc = model.StartDateUtc ?? DateTime.UtcNow,
                    EndDateUtc = model.EndDateUtc ?? DateTime.UtcNow.AddDays(7),
                    IsActive = model.IsActive,
                    InitialQuantity = model.InitialQuantity,
                    RemainingQuantity = model.RemainingQuantity > 0 ? model.RemainingQuantity : model.InitialQuantity,
                    ShowRemainingStock = model.ShowRemainingStock,
                    ShowSoldCount = model.ShowSoldCount,
                    ShowProgressBar = model.ShowProgressBar,
                    ProgressBarMode = model.ProgressBarMode,
                    DiscountPercentage = model.DiscountPercentage,
                    BlockPurchaseWhenExpired = model.BlockPurchaseWhenExpired
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

            if (int.TryParse(term, out int productId))
            {
                var product = await _productService.GetProductByIdAsync(productId);
                if (product != null)
                {
                    result.Add(new { id = product.Id, label = $"{product.Name} [ID: {product.Id}]", value = product.Name });
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
                visibleIndividuallyOnly: false);

            foreach (var p in products)
                result.Add(new { id = p.Id, label = $"{p.Name} [ID: {p.Id}]", value = p.Name });

            return Json(result);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> ExportTemplates()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return AccessDeniedView();
            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);
            var payload = new
            {
                cardTemplatesJson = settings.CardTemplatesJson,
                popupTemplatesJson = settings.PopupTemplatesJson,
                selectedCardTemplateId = settings.SelectedCardTemplateId,
                selectedPopupTemplateId = settings.SelectedPopupTemplateId
            };
            var json = System.Text.Json.JsonSerializer.Serialize(payload, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            return File(bytes, "application/json", "limited-edition-templates.json");
        }

        [HttpPost]
        public async Task<IActionResult> ImportTemplates(string templatesJson)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { success = false });
            if (string.IsNullOrWhiteSpace(templatesJson))
                return Json(new { success = false, error = "empty" });
            try
            {
                using var doc = System.Text.Json.JsonDocument.Parse(templatesJson);
                var root = doc.RootElement;
                var store = await _storeContext.GetCurrentStoreAsync();
                var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);
                if (root.TryGetProperty("cardTemplatesJson", out var c))
                    settings.CardTemplatesJson = c.GetString();
                if (root.TryGetProperty("popupTemplatesJson", out var p))
                    settings.PopupTemplatesJson = p.GetString();
                if (root.TryGetProperty("selectedCardTemplateId", out var sc))
                    settings.SelectedCardTemplateId = sc.GetInt32();
                if (root.TryGetProperty("selectedPopupTemplateId", out var sp))
                    settings.SelectedPopupTemplateId = sp.GetInt32();
                await _settingService.SaveSettingAsync<LimitedTimeSettings>(settings, store.Id);
                await _settingService.ClearCacheAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
