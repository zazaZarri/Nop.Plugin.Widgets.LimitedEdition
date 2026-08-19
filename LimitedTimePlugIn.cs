using Nop.Core;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Plugin.Widgets.LimitedEdition.Models;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.LimitedEdition
{
    public class LimitedTimePlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public LimitedTimePlugin(
            IWebHelper webHelper,
            ISettingService settingService,
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _settingService = settingService;
            _localizationService = localizationService;
        }

        public bool HideInWidgetList => false;

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {
                PublicWidgetZones.HomepageTop,
                PublicWidgetZones.HomepageBeforeProducts,
                PublicWidgetZones.ProductDetailsTop,
                PublicWidgetZones.OrderSummaryContentBefore
            });
        }

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(Components.WidgetsLimitedEditionViewComponent);
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/LimitedTime/Configure";
        }

        public override async Task InstallAsync()
        {
            var cardDict = StyleBag.EnsureCardTemplates(null);
            var popupDict = StyleBag.EnsurePopupTemplates(null);

            // Default settings in English (neutral). Store owners can change texts in admin.
            // Public-facing labels also live in locale resources so they follow the active language.
            await _settingService.SaveSettingAsync(new LimitedTimeSettings
            {
                SelectedCardTemplateId = 0,
                SelectedPopupTemplateId = 0,
                CardTemplatesJson = StyleBag.SerializeDict(cardDict),
                PopupTemplatesJson = StyleBag.SerializeDict(popupDict),

                CustomMessage = "Limited-time product! 500 copies left",
                BadgeText = "⚡ Limited Edition",
                CtaText = "See offer →",
                ExpiredText = "Offer expired!",
                DaysLabel = "Days",
                HoursLabel = "Hours",
                MinutesLabel = "Min",
                SecondsLabel = "Sec",

                TimerLayout = TimerLayoutType.Horizontal,
                HideProductWhenExpired = false,
                ShowBadge = true,
                ShowTitle = true,
                ShowMessage = true,
                ShowCta = true,
                EnableGlowAnimation = true,
                EnableSheenAnimation = true,
                CardTextAlign = "center",

                EnableCartPopup = true,
                EnableSocialProof = true,
                SocialProofIntervalSeconds = 12,
                SocialProofIncludeSimulated = true,
                EnableDynamicBadges = true,
                DynamicBadgeUrgentHours = 24,
                DynamicBadgeLowStockPercent = 20,
                EnableLastHourSound = false,
                EnableAbTest = false,
                EnableExpiryReminders = false,
                ReminderHoursBeforeExpiry = 6,
                CompactOnProductPage = true,
                PreferStoryOnHomepageTop = false,
                UseProductImageAsBackground = false,
                GlobalBlockPurchaseWhenExpired = true,
                DefaultShowProgressBar = true,
                DefaultProgressBarMode = 0,
                EnableServerCountdown = true,
                PopupShowDelayMs = 500,
                PopupOncePerSession = true,
                PopupCloseOnOverlayClick = true,
                PopupCloseOnEscape = true,

                // Legacy defaults (Classic)
                AccentColor = "d4af37",
                AccentColorLight = "#f4e5a1",
                CardBackgroundStart = "#0b0b0f",
                CardBackgroundMid = "#1a1a22",
                CardBackgroundEnd = "#0b0b0f",
                BorderColor = "rgba(212, 175, 55, 0.45)",
                TitleColor = "#ffffff",
                MessageColor = "rgba(255, 255, 255, 0.72)",
                BadgeTextColor = "#1a1a22",
                BadgeBackgroundStart = "#f4e5a1",
                BadgeBackgroundEnd = "#d4af37",
                TimerDigitColor = "#f4e5a1",
                TimerLabelColor = "rgba(255, 255, 255, 0.45)",
                TimerBoxBackground = "rgba(212, 175, 55, 0.08)",
                TimerBoxBorderColor = "rgba(212, 175, 55, 0.35)",
                CtaBackgroundStart = "#d4af37",
                CtaBackgroundEnd = "#f4e5a1",
                CtaTextColor = "#1a1a22",
                TimerDigitFontSize = 26,
                TimerLabelFontSize = 9,
                TimerBoxMinWidth = 58,
                TimerBoxBorderRadius = 10,
                TimerBoxPadding = 8,
                TimerGap = 10,
                CardBorderRadius = 16,
                CardPaddingTop = 28,
                CardPaddingSide = 24,
                CardPaddingBottom = 24,
                CardMarginBottom = 22,
                CardBorderWidth = 1,
                TitleFontSize = 22,
                TitleFontWeight = 700,
                MessageFontSize = 14,
                BadgeFontSize = 11,
                BadgeFontWeight = 800,
                BadgeLetterSpacing = 2,
                BadgePaddingY = 5,
                BadgePaddingX = 14,
                CtaFontSize = 14,
                CtaFontWeight = 700,
                CtaPaddingY = 11,
                CtaPaddingX = 28,
                CtaBorderRadius = 999,
                BackgroundColor = "#d4af37",
                TextColor = "#ffffff",
                FontFamily = "inherit",

                PopupTitle = "Don't miss the limited edition!",
                PopupSubtitle = "These exclusive products are about to expire",
                PopupContinueText = "Continue shopping",
                PopupShowBadge = true,
                PopupShowProductList = true,
                PopupEnableGlow = true,
                PopupEnableSheen = false,
                PopupAnimationType = 1,
                PopupAnimationDurationMs = 350,
                PopupOverlayOpacity = 72,
                PopupOverlayBlurPx = 6,
                PopupModalMaxWidth = 420
            });

            await InstallLocaleResourcesAsync();

            await base.InstallAsync();
        }

        /// <summary>
        /// Registers English as default for ALL languages, then culture-specific overrides
        /// (Italian, and any other dictionaries defined in LocaleResources.ByCulture).
        /// Store owners can further edit resources in Admin → Configuration → Languages.
        /// </summary>
        public async Task InstallLocaleResourcesAsync()
        {
            // 1) English defaults → applied to every existing language in the store
            await _localizationService.AddOrUpdateLocaleResourceAsync(LocaleResources.English);

            // 2) Culture-specific overrides (only if that language culture exists)
            foreach (var pair in LocaleResources.ByCulture)
            {
                var culture = pair.Key;
                var resources = pair.Value;
                foreach (var resource in resources)
                {
                    await _localizationService.AddOrUpdateLocaleResourceAsync(
                        resource.Key,
                        resource.Value,
                        culture);
                }
            }
        }

        public override async Task UninstallAsync()
        {
            try
            {
                await _settingService.DeleteSettingAsync<LimitedTimeSettings>();
            }
            catch
            {
                // settings già assenti: ok
            }

            await base.UninstallAsync();
        }
    }
}
