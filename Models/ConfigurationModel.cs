using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Modello della pagina di configurazione admin.
    /// I campi di stile si riferiscono sempre al template attualmente selezionato.
    /// </summary>
    public partial record ConfigurationModel : BaseNopModel
    {
        public ConfigurationModel()
        {
            LimitedTimeProductSearchModel = new LimitedTimeProductSearchModel();
        }

        public LimitedTimeProductSearchModel LimitedTimeProductSearchModel { get; set; }

        // ── Template selezionati ───────────────────────────────────────────
        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.SelectedCardTemplate")]
        public int SelectedCardTemplateId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.SelectedPopupTemplate")]
        public int SelectedPopupTemplateId { get; set; }

        /// <summary>JSON completo dei template card (hidden, per round-trip).</summary>
        public string CardTemplatesJson { get; set; }

        /// <summary>JSON completo dei template popup (hidden, per round-trip).</summary>
        public string PopupTemplatesJson { get; set; }

        // ── Testi ──────────────────────────────────────────────────────────
        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CustomMessage")]
        public string CustomMessage { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgeText")]
        public string BadgeText { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaText")]
        public string CtaText { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.ExpiredText")]
        public string ExpiredText { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.DaysLabel")]
        public string DaysLabel { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.HoursLabel")]
        public string HoursLabel { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.MinutesLabel")]
        public string MinutesLabel { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.SecondsLabel")]
        public string SecondsLabel { get; set; }

        // ── Layout ─────────────────────────────────────────────────────────
        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerLayout")]
        public int TimerLayoutId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.HideProductWhenExpired")]
        public bool HideProductWhenExpired { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.ShowBadge")]
        public bool ShowBadge { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.ShowTitle")]
        public bool ShowTitle { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.ShowMessage")]
        public bool ShowMessage { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.ShowCta")]
        public bool ShowCta { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.EnableGlowAnimation")]
        public bool EnableGlowAnimation { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.EnableSheenAnimation")]
        public bool EnableSheenAnimation { get; set; }

        public string FontFamily { get; set; }

        // ── Colori ─────────────────────────────────────────────────────────
        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.AccentColor")]
        public string AccentColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.AccentColorLight")]
        public string AccentColorLight { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardBackgroundStart")]
        public string CardBackgroundStart { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardBackgroundMid")]
        public string CardBackgroundMid { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardBackgroundEnd")]
        public string CardBackgroundEnd { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BorderColor")]
        public string BorderColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TitleColor")]
        public string TitleColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.MessageColor")]
        public string MessageColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgeTextColor")]
        public string BadgeTextColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgeBackgroundStart")]
        public string BadgeBackgroundStart { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgeBackgroundEnd")]
        public string BadgeBackgroundEnd { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerDigitColor")]
        public string TimerDigitColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerLabelColor")]
        public string TimerLabelColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerBoxBackground")]
        public string TimerBoxBackground { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerBoxBorderColor")]
        public string TimerBoxBorderColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaBackgroundStart")]
        public string CtaBackgroundStart { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaBackgroundEnd")]
        public string CtaBackgroundEnd { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaTextColor")]
        public string CtaTextColor { get; set; }

        // ── Dimensioni ─────────────────────────────────────────────────────
        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerDigitFontSize")]
        public int TimerDigitFontSize { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerLabelFontSize")]
        public int TimerLabelFontSize { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerBoxMinWidth")]
        public int TimerBoxMinWidth { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerBoxBorderRadius")]
        public int TimerBoxBorderRadius { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerBoxPadding")]
        public int TimerBoxPadding { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerGap")]
        public int TimerGap { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardBorderRadius")]
        public int CardBorderRadius { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardPaddingTop")]
        public int CardPaddingTop { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardPaddingSide")]
        public int CardPaddingSide { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardPaddingBottom")]
        public int CardPaddingBottom { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardMarginBottom")]
        public int CardMarginBottom { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardBorderWidth")]
        public int CardBorderWidth { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CardTextAlign")]
        public string CardTextAlign { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TitleFontSize")]
        public int TitleFontSize { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TitleFontWeight")]
        public int TitleFontWeight { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.MessageFontSize")]
        public int MessageFontSize { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgeFontSize")]
        public int BadgeFontSize { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgeFontWeight")]
        public int BadgeFontWeight { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgeLetterSpacing")]
        public int BadgeLetterSpacing { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgePaddingY")]
        public int BadgePaddingY { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BadgePaddingX")]
        public int BadgePaddingX { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaFontSize")]
        public int CtaFontSize { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaFontWeight")]
        public int CtaFontWeight { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaPaddingY")]
        public int CtaPaddingY { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaPaddingX")]
        public int CtaPaddingX { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CtaBorderRadius")]
        public int CtaBorderRadius { get; set; }

        // Legacy
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public int ActiveStoreScopeConfiguration { get; set; }

        // ── Popup comportamento + stile del template selezionato ───────────
        public bool EnableCartPopup { get; set; }
        public int PopupShowDelayMs { get; set; }
        public bool PopupOncePerSession { get; set; }
        public int PopupAnimationType { get; set; }
        public int PopupAnimationDurationMs { get; set; }
        public bool PopupCloseOnOverlayClick { get; set; }
        public bool PopupCloseOnEscape { get; set; }
        public int PopupOverlayOpacity { get; set; }
        public int PopupOverlayBlurPx { get; set; }
        public int PopupModalMaxWidth { get; set; }
        public string PopupTitle { get; set; }
        public string PopupSubtitle { get; set; }
        public string PopupContinueText { get; set; }
        public bool PopupShowBadge { get; set; }
        public bool PopupShowProductList { get; set; }
        public bool PopupEnableGlow { get; set; }
        public bool PopupEnableSheen { get; set; }

        // Feature flags
        public bool EnableSocialProof { get; set; }
        public int SocialProofIntervalSeconds { get; set; }
        public bool SocialProofIncludeSimulated { get; set; }
        public bool EnableDynamicBadges { get; set; }
        public int DynamicBadgeUrgentHours { get; set; }
        public int DynamicBadgeLowStockPercent { get; set; }
        public bool EnableLastHourSound { get; set; }
        public bool EnableAbTest { get; set; }
        public string AbTestTemplateIds { get; set; }
        public bool EnableExpiryReminders { get; set; }
        public int ReminderHoursBeforeExpiry { get; set; }
        public bool CompactOnProductPage { get; set; }
        public bool PreferStoryOnHomepageTop { get; set; }
        public bool UseProductImageAsBackground { get; set; }
        public bool GlobalBlockPurchaseWhenExpired { get; set; }
        public bool DefaultShowProgressBar { get; set; }
        public int DefaultProgressBarMode { get; set; }
        public bool EnableServerCountdown { get; set; }
    }
}
