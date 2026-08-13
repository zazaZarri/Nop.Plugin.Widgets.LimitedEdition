using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Snapshot completo di stile usato dalle view pubbliche.
    /// </summary>
    public class StyleSettingsModel
    {
        public int CardTemplateId { get; set; }
        public int PopupTemplateId { get; set; }

        // Testi
        public string BadgeText { get; set; }
        public string CtaText { get; set; }
        public string ExpiredText { get; set; }
        public string DaysLabel { get; set; }
        public string HoursLabel { get; set; }
        public string MinutesLabel { get; set; }
        public string SecondsLabel { get; set; }

        // Visibilità
        public bool ShowBadge { get; set; }
        public bool ShowTitle { get; set; }
        public bool ShowMessage { get; set; }
        public bool ShowCta { get; set; }
        public bool EnableGlowAnimation { get; set; }
        public bool EnableSheenAnimation { get; set; }
        public TimerLayoutType TimerLayout { get; set; }

        // Colori
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

        // Dimensioni
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
        public string FontFamily { get; set; }

        // Popup
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

        /// <summary>
        /// Costruisce lo stile dalla settings "flat" (sempre allineato all'ultimo salvataggio admin).
        /// </summary>
        public static StyleSettingsModel FromLimitedTimeSettings(Nop.Plugin.Widgets.LimitedEdition.Domain.LimitedTimeSettings s)
        {
            if (s == null) s = new Nop.Plugin.Widgets.LimitedEdition.Domain.LimitedTimeSettings();
            string D(string v, string fb) => string.IsNullOrWhiteSpace(v) ? fb : v;
            int Ip(int v, int fb) => v > 0 ? v : fb;
            int Iz(int v, int fb) => v >= 0 ? v : fb;

            return new StyleSettingsModel
            {
                CardTemplateId = s.SelectedCardTemplateId,
                PopupTemplateId = s.SelectedPopupTemplateId,

                BadgeText = D(s.BadgeText, "⚡ Edizione Limitata"),
                CtaText = D(s.CtaText, "Vedi offerta →"),
                ExpiredText = D(s.ExpiredText, "Offerta scaduta!"),
                DaysLabel = D(s.DaysLabel, "Giorni"),
                HoursLabel = D(s.HoursLabel, "Ore"),
                MinutesLabel = D(s.MinutesLabel, "Min"),
                SecondsLabel = D(s.SecondsLabel, "Sec"),

                ShowBadge = s.ShowBadge,
                ShowTitle = s.ShowTitle,
                ShowMessage = s.ShowMessage,
                ShowCta = s.ShowCta,
                EnableGlowAnimation = s.EnableGlowAnimation,
                EnableSheenAnimation = s.EnableSheenAnimation,
                TimerLayout = s.TimerLayout,

                AccentColor = D(s.AccentColor, "#d4af37"),
                AccentColorLight = D(s.AccentColorLight, "#f4e5a1"),
                CardBackgroundStart = D(s.CardBackgroundStart, "#0b0b0f"),
                CardBackgroundMid = D(s.CardBackgroundMid, "#1a1a22"),
                CardBackgroundEnd = D(s.CardBackgroundEnd, "#0b0b0f"),
                BorderColor = D(s.BorderColor, "rgba(212, 175, 55, 0.45)"),
                TitleColor = D(s.TitleColor, "#ffffff"),
                MessageColor = D(s.MessageColor, "rgba(255, 255, 255, 0.72)"),
                BadgeTextColor = D(s.BadgeTextColor, "#1a1a22"),
                BadgeBackgroundStart = D(s.BadgeBackgroundStart, "#f4e5a1"),
                BadgeBackgroundEnd = D(s.BadgeBackgroundEnd, "#d4af37"),
                TimerDigitColor = D(s.TimerDigitColor, "#f4e5a1"),
                TimerLabelColor = D(s.TimerLabelColor, "rgba(255, 255, 255, 0.45)"),
                TimerBoxBackground = D(s.TimerBoxBackground, "rgba(212, 175, 55, 0.08)"),
                TimerBoxBorderColor = D(s.TimerBoxBorderColor, "rgba(212, 175, 55, 0.35)"),
                CtaBackgroundStart = D(s.CtaBackgroundStart, "#d4af37"),
                CtaBackgroundEnd = D(s.CtaBackgroundEnd, "#f4e5a1"),
                CtaTextColor = D(s.CtaTextColor, "#1a1a22"),
                FontFamily = D(s.FontFamily, "inherit"),

                TimerDigitFontSize = Ip(s.TimerDigitFontSize, 26),
                TimerLabelFontSize = Ip(s.TimerLabelFontSize, 9),
                TimerBoxMinWidth = Ip(s.TimerBoxMinWidth, 58),
                TimerBoxBorderRadius = Iz(s.TimerBoxBorderRadius, 10),
                TimerBoxPadding = Iz(s.TimerBoxPadding, 8),
                TimerGap = Iz(s.TimerGap, 10),
                CardBorderRadius = Iz(s.CardBorderRadius, 16),
                CardPaddingTop = Iz(s.CardPaddingTop, 28),
                CardPaddingSide = Iz(s.CardPaddingSide, 24),
                CardPaddingBottom = Iz(s.CardPaddingBottom, 24),
                CardMarginBottom = Iz(s.CardMarginBottom, 22),
                CardBorderWidth = Iz(s.CardBorderWidth, 1),
                CardTextAlign = D(s.CardTextAlign, "center"),
                TitleFontSize = Ip(s.TitleFontSize, 22),
                TitleFontWeight = Ip(s.TitleFontWeight, 700),
                MessageFontSize = Ip(s.MessageFontSize, 14),
                BadgeFontSize = Ip(s.BadgeFontSize, 11),
                BadgeFontWeight = Ip(s.BadgeFontWeight, 800),
                BadgeLetterSpacing = Iz(s.BadgeLetterSpacing, 2),
                BadgePaddingY = Iz(s.BadgePaddingY, 5),
                BadgePaddingX = Iz(s.BadgePaddingX, 14),
                CtaFontSize = Ip(s.CtaFontSize, 14),
                CtaFontWeight = Ip(s.CtaFontWeight, 700),
                CtaPaddingY = Iz(s.CtaPaddingY, 11),
                CtaPaddingX = Iz(s.CtaPaddingX, 28),
                CtaBorderRadius = Iz(s.CtaBorderRadius, 999),

                EnableCartPopup = s.EnableCartPopup,
                PopupShowDelayMs = Ip(s.PopupShowDelayMs, 500),
                PopupOncePerSession = s.PopupOncePerSession,
                PopupAnimationType = s.PopupAnimationType,
                PopupAnimationDurationMs = Ip(s.PopupAnimationDurationMs, 350),
                PopupCloseOnOverlayClick = s.PopupCloseOnOverlayClick,
                PopupCloseOnEscape = s.PopupCloseOnEscape,
                PopupOverlayOpacity = Ip(s.PopupOverlayOpacity, 72),
                PopupOverlayBlurPx = Iz(s.PopupOverlayBlurPx, 6),
                PopupModalMaxWidth = Ip(s.PopupModalMaxWidth, 420),
                PopupTitle = D(s.PopupTitle, "Non perderti l'edizione limitata!"),
                PopupSubtitle = D(s.PopupSubtitle, "Questi prodotti esclusivi stanno per scadere"),
                PopupContinueText = D(s.PopupContinueText, "Continua lo shopping"),
                PopupShowBadge = s.PopupShowBadge,
                PopupShowProductList = s.PopupShowProductList,
                PopupEnableGlow = s.PopupEnableGlow,
                PopupEnableSheen = s.PopupEnableSheen
            };
        }

    }
}