using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.LimitedEdition.Domain
{
    /// <summary>
    /// Impostazioni complete del widget Limited Edition.
    /// Ogni aspetto visivo (colori, dimensioni, tipografia, layout, testi) è configurabile.
    /// </summary>
    public class LimitedTimeSettings : ISettings
    {
        // ── Testi ──────────────────────────────────────────────────────────
        public string CustomMessage { get; set; }
        public string BadgeText { get; set; }
        public string CtaText { get; set; }
        public string ExpiredText { get; set; }
        public string DaysLabel { get; set; }
        public string HoursLabel { get; set; }
        public string MinutesLabel { get; set; }
        public string SecondsLabel { get; set; }

        // ── Layout generale ────────────────────────────────────────────────
        public TimerLayoutType TimerLayout { get; set; }
        public bool HideProductWhenExpired { get; set; }
        public bool ShowBadge { get; set; }
        public bool ShowTitle { get; set; }
        public bool ShowMessage { get; set; }
        public bool ShowCta { get; set; }
        public bool EnableGlowAnimation { get; set; }
        public bool EnableSheenAnimation { get; set; }

        // ── Colori principali ──────────────────────────────────────────────
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

        // ── Timer ──────────────────────────────────────────────────────────
        public string TimerDigitColor { get; set; }
        public string TimerLabelColor { get; set; }
        public string TimerBoxBackground { get; set; }
        public string TimerBoxBorderColor { get; set; }
        public int TimerDigitFontSize { get; set; }
        public int TimerLabelFontSize { get; set; }
        public int TimerBoxMinWidth { get; set; }
        public int TimerBoxBorderRadius { get; set; }
        public int TimerBoxPadding { get; set; }
        public int TimerGap { get; set; }

        // ── Card / contenitore ─────────────────────────────────────────────
        public int CardBorderRadius { get; set; }
        public int CardPaddingTop { get; set; }
        public int CardPaddingSide { get; set; }
        public int CardPaddingBottom { get; set; }
        public int CardMarginBottom { get; set; }
        public int CardBorderWidth { get; set; }
        public string CardTextAlign { get; set; }

        // ── Tipografia ─────────────────────────────────────────────────────
        public int TitleFontSize { get; set; }
        public int TitleFontWeight { get; set; }
        public int MessageFontSize { get; set; }
        public int BadgeFontSize { get; set; }
        public int BadgeFontWeight { get; set; }
        public int BadgeLetterSpacing { get; set; }
        public int BadgePaddingY { get; set; }
        public int BadgePaddingX { get; set; }

        // ── CTA ────────────────────────────────────────────────────────────
        public string CtaBackgroundStart { get; set; }
        public string CtaBackgroundEnd { get; set; }
        public string CtaTextColor { get; set; }
        public int CtaFontSize { get; set; }
        public int CtaFontWeight { get; set; }
        public int CtaPaddingY { get; set; }
        public int CtaPaddingX { get; set; }
        public int CtaBorderRadius { get; set; }

        // ── Compatibilità legacy (mappati su Accent / Message) ─────────────
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }

        // ── Popup carrello ─────────────────────────────────────────────────
        public bool EnableCartPopup { get; set; }
        public int PopupShowDelayMs { get; set; }
        public bool PopupOncePerSession { get; set; }
        public int PopupAnimationType { get; set; } // 0=fade, 1=scale, 2=slideUp, 3=slideDown
        public int PopupAnimationDurationMs { get; set; }
        public bool PopupCloseOnOverlayClick { get; set; }
        public bool PopupCloseOnEscape { get; set; }
        public int PopupOverlayOpacity { get; set; } // 0-100
        public int PopupOverlayBlurPx { get; set; }
        public int PopupModalMaxWidth { get; set; }
        public string PopupTitle { get; set; }
        public string PopupSubtitle { get; set; }
        public string PopupContinueText { get; set; }
        public bool PopupShowBadge { get; set; }
        public bool PopupShowProductList { get; set; }
        public bool PopupEnableGlow { get; set; }
        public bool PopupEnableSheen { get; set; }

        // FONT

        public string FontFamily { get; set; }
    }
}
