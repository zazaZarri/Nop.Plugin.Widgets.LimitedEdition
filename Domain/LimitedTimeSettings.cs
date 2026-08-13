using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.LimitedEdition.Domain
{
    public class LimitedTimeSettings : ISettings
    {
        public int SelectedCardTemplateId { get; set; }
        public int SelectedPopupTemplateId { get; set; }
        public string CardTemplatesJson { get; set; }
        public string PopupTemplatesJson { get; set; }

        public string CustomMessage { get; set; }
        public string BadgeText { get; set; }
        public string CtaText { get; set; }
        public string ExpiredText { get; set; }
        public string DaysLabel { get; set; }
        public string HoursLabel { get; set; }
        public string MinutesLabel { get; set; }
        public string SecondsLabel { get; set; }

        public TimerLayoutType TimerLayout { get; set; }
        public bool HideProductWhenExpired { get; set; }
        public bool ShowBadge { get; set; }
        public bool ShowTitle { get; set; }
        public bool ShowMessage { get; set; }
        public bool ShowCta { get; set; }
        public bool EnableGlowAnimation { get; set; }
        public bool EnableSheenAnimation { get; set; }

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

        public string AccentColor { get; set; }
        public string AccentColorLight { get; set; }
        public string CardBackgroundStart { get; set; }
        public string CardBackgroundMid { get; set; }
        public string CardBackgroundEnd { get; set; }
        public string BorderColor { get; set; }
        public string TitleColor { get; set; }
        public string MessageColor { get; set; }
        public string BadgeTextColor { get; set; }
        public string BadgeBackgroundStart { get; set; }
        public string BadgeBackgroundEnd { get; set; }
        public string TimerDigitColor { get; set; }
        public string TimerLabelColor { get; set; }
        public string TimerBoxBackground { get; set; }
        public string TimerBoxBorderColor { get; set; }
        public string CtaBackgroundStart { get; set; }
        public string CtaBackgroundEnd { get; set; }
        public string CtaTextColor { get; set; }
        public int TimerDigitFontSize { get; set; }
        public int TimerLabelFontSize { get; set; }
        public int TimerBoxMinWidth { get; set; }
        public int TimerBoxBorderRadius { get; set; }
        public int TimerBoxPadding { get; set; }
        public int TimerGap { get; set; }
        public int CardBorderRadius { get; set; }
        public int CardPaddingTop { get; set; }
        public int CardPaddingSide { get; set; }
        public int CardPaddingBottom { get; set; }
        public int CardMarginBottom { get; set; }
        public int CardBorderWidth { get; set; }
        public string CardTextAlign { get; set; }
        public int TitleFontSize { get; set; }
        public int TitleFontWeight { get; set; }
        public int MessageFontSize { get; set; }
        public int BadgeFontSize { get; set; }
        public int BadgeFontWeight { get; set; }
        public int BadgeLetterSpacing { get; set; }
        public int BadgePaddingY { get; set; }
        public int BadgePaddingX { get; set; }
        public int CtaFontSize { get; set; }
        public int CtaFontWeight { get; set; }
        public int CtaPaddingY { get; set; }
        public int CtaPaddingX { get; set; }
        public int CtaBorderRadius { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public string FontFamily { get; set; }

        // Feature flags
        public bool EnableSocialProof { get; set; } = true;
        public int SocialProofIntervalSeconds { get; set; } = 12;
        public bool SocialProofIncludeSimulated { get; set; } = true;
        public bool EnableDynamicBadges { get; set; } = true;
        public int DynamicBadgeUrgentHours { get; set; } = 24;
        public int DynamicBadgeLowStockPercent { get; set; } = 20;
        public bool EnableLastHourSound { get; set; }
        public bool EnableAbTest { get; set; }
        public string AbTestTemplateIds { get; set; }
        public bool EnableExpiryReminders { get; set; }
        public int ReminderHoursBeforeExpiry { get; set; } = 6;
        public bool CompactOnProductPage { get; set; } = true;
        public bool PreferStoryOnHomepageTop { get; set; }
        /// <summary>Usa l'immagine prodotto come sfondo della card (soprattutto Story / homepage).</summary>
        public bool UseProductImageAsBackground { get; set; }
        public bool GlobalBlockPurchaseWhenExpired { get; set; } = true;
        public bool DefaultShowProgressBar { get; set; } = true;
        public int DefaultProgressBarMode { get; set; }
        public bool EnableServerCountdown { get; set; } = true;
    }
}
