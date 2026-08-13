using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            await _settingService.SaveSettingAsync(new LimitedTimeSettings
            {
                SelectedCardTemplateId = 0,
                SelectedPopupTemplateId = 0,
                CardTemplatesJson = StyleBag.SerializeDict(cardDict),
                PopupTemplatesJson = StyleBag.SerializeDict(popupDict),

                CustomMessage = "Prodotto a tempo limitato! 500 copie rimaste",
                BadgeText = "⚡ Edizione Limitata",
                CtaText = "Vedi offerta →",
                ExpiredText = "Offerta scaduta!",
                DaysLabel = "Giorni",
                HoursLabel = "Ore",
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
                AccentColor = "#d4af37",
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
                FontFamily = "inherit"
            });

            var resources = new Dictionary<string, string>
            {
                ["Plugins.Widgets.LimitedEdition.Settings.CustomMessage"] = "Messaggio personalizzato",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgeText"] = "Testo badge",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaText"] = "Testo pulsante CTA",
                ["Plugins.Widgets.LimitedEdition.Settings.ExpiredText"] = "Testo offerta scaduta",
                ["Plugins.Widgets.LimitedEdition.Settings.DaysLabel"] = "Etichetta giorni",
                ["Plugins.Widgets.LimitedEdition.Settings.HoursLabel"] = "Etichetta ore",
                ["Plugins.Widgets.LimitedEdition.Settings.MinutesLabel"] = "Etichetta minuti",
                ["Plugins.Widgets.LimitedEdition.Settings.SecondsLabel"] = "Etichetta secondi",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerLayout"] = "Layout timer",
                ["Plugins.Widgets.LimitedEdition.Settings.HideProductWhenExpired"] = "Nascondi prodotto scaduto",
                ["Plugins.Widgets.LimitedEdition.Settings.ShowBadge"] = "Mostra badge",
                ["Plugins.Widgets.LimitedEdition.Settings.ShowTitle"] = "Mostra titolo prodotto",
                ["Plugins.Widgets.LimitedEdition.Settings.ShowMessage"] = "Mostra messaggio",
                ["Plugins.Widgets.LimitedEdition.Settings.ShowCta"] = "Mostra pulsante CTA",
                ["Plugins.Widgets.LimitedEdition.Settings.EnableGlowAnimation"] = "Animazione glow",
                ["Plugins.Widgets.LimitedEdition.Settings.EnableSheenAnimation"] = "Animazione sheen",
                ["Plugins.Widgets.LimitedEdition.Settings.AccentColor"] = "Colore accento",
                ["Plugins.Widgets.LimitedEdition.Settings.AccentColorLight"] = "Colore accento chiaro",
                ["Plugins.Widgets.LimitedEdition.Settings.CardBackgroundStart"] = "Sfondo card (inizio)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardBackgroundMid"] = "Sfondo card (mezzo)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardBackgroundEnd"] = "Sfondo card (fine)",
                ["Plugins.Widgets.LimitedEdition.Settings.BorderColor"] = "Colore bordo",
                ["Plugins.Widgets.LimitedEdition.Settings.TitleColor"] = "Colore titolo",
                ["Plugins.Widgets.LimitedEdition.Settings.MessageColor"] = "Colore messaggio",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgeTextColor"] = "Colore testo badge",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgeBackgroundStart"] = "Sfondo badge (inizio)",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgeBackgroundEnd"] = "Sfondo badge (fine)",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerDigitColor"] = "Colore cifre timer",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerLabelColor"] = "Colore etichette timer",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerBoxBackground"] = "Sfondo box timer",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerBoxBorderColor"] = "Bordo box timer",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerDigitFontSize"] = "Dimensione cifre (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerLabelFontSize"] = "Dimensione etichette (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerBoxMinWidth"] = "Larghezza minima box (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerBoxBorderRadius"] = "Raggio angoli box (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerBoxPadding"] = "Padding box (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.TimerGap"] = "Spazio tra box (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardBorderRadius"] = "Raggio angoli card (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardPaddingTop"] = "Padding alto card (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardPaddingSide"] = "Padding laterale card (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardPaddingBottom"] = "Padding basso card (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardMarginBottom"] = "Margine inferiore card (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardBorderWidth"] = "Spessore bordo card (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CardTextAlign"] = "Allineamento testo",
                ["Plugins.Widgets.LimitedEdition.Settings.TitleFontSize"] = "Dimensione titolo (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.TitleFontWeight"] = "Peso font titolo",
                ["Plugins.Widgets.LimitedEdition.Settings.MessageFontSize"] = "Dimensione messaggio (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgeFontSize"] = "Dimensione badge (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgeFontWeight"] = "Peso font badge",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgeLetterSpacing"] = "Spaziatura lettere badge (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgePaddingY"] = "Padding verticale badge (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.BadgePaddingX"] = "Padding orizzontale badge (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaBackgroundStart"] = "Sfondo CTA (inizio)",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaBackgroundEnd"] = "Sfondo CTA (fine)",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaTextColor"] = "Colore testo CTA",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaFontSize"] = "Dimensione testo CTA (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaFontWeight"] = "Peso font CTA",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaPaddingY"] = "Padding verticale CTA (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaPaddingX"] = "Padding orizzontale CTA (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.CtaBorderRadius"] = "Raggio angoli CTA (px)",
                ["Plugins.Widgets.LimitedEdition.Settings.BackgroundColor"] = "Colore sfondo (legacy)",
                ["Plugins.Widgets.LimitedEdition.Settings.TextColor"] = "Colore testo (legacy)",
                ["Plugins.Widgets.LimitedEdition.Settings.SelectedCardTemplate"] = "Template card",
                ["Plugins.Widgets.LimitedEdition.Settings.SelectedPopupTemplate"] = "Template popup"
            };

            foreach (var resource in resources)
            {
                await _localizationService.AddOrUpdateLocaleResourceAsync(resource.Key, resource.Value, "it-IT");
            }

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<LimitedTimeSettings>();
            await base.UninstallAsync();
        }
    }
}
