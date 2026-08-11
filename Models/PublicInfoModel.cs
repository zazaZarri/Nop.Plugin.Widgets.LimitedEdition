using System;
using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    public class PublicInfoModel
    {
        public int ProductId { get; set; }

        public DateTime EndDateUtc { get; set; }

        public string CustomMessage { get; set; }

        public string ProductName { get; set; }

        public string ProductUrl { get; set; }

        /// <summary>
        /// Snapshot completo delle impostazioni di stile da applicare al widget.
        /// </summary>
        public StyleSettingsModel Style { get; set; }
    }

    /// <summary>
    /// Tutte le proprietà CSS/testuali necessarie per renderizzare il widget
    /// senza dover rileggere le settings in ogni partial.
    /// </summary>
    public class StyleSettingsModel
    {
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

        public static StyleSettingsModel FromSettings(LimitedTimeSettings s)
        {
            if (s == null) s = new LimitedTimeSettings();

            return new StyleSettingsModel
            {
                BadgeText = string.IsNullOrWhiteSpace(s.BadgeText) ? "⚡ Edizione Limitata" : s.BadgeText,
                CtaText = string.IsNullOrWhiteSpace(s.CtaText) ? "Vedi offerta →" : s.CtaText,
                ExpiredText = string.IsNullOrWhiteSpace(s.ExpiredText) ? "Offerta scaduta!" : s.ExpiredText,
                DaysLabel = string.IsNullOrWhiteSpace(s.DaysLabel) ? "Giorni" : s.DaysLabel,
                HoursLabel = string.IsNullOrWhiteSpace(s.HoursLabel) ? "Ore" : s.HoursLabel,
                MinutesLabel = string.IsNullOrWhiteSpace(s.MinutesLabel) ? "Min" : s.MinutesLabel,
                SecondsLabel = string.IsNullOrWhiteSpace(s.SecondsLabel) ? "Sec" : s.SecondsLabel,

                ShowBadge = s.ShowBadge,
                ShowTitle = s.ShowTitle,
                ShowMessage = s.ShowMessage,
                ShowCta = s.ShowCta,
                EnableGlowAnimation = s.EnableGlowAnimation,
                EnableSheenAnimation = s.EnableSheenAnimation,
                TimerLayout = s.TimerLayout,

                AccentColor = Def(s.AccentColor, "#d4af37"),
                AccentColorLight = Def(s.AccentColorLight, "#f4e5a1"),
                CardBackgroundStart = Def(s.CardBackgroundStart, "#0b0b0f"),
                CardBackgroundMid = Def(s.CardBackgroundMid, "#1a1a22"),
                CardBackgroundEnd = Def(s.CardBackgroundEnd, "#0b0b0f"),
                BorderColor = Def(s.BorderColor, "rgba(212, 175, 55, 0.45)"),
                TitleColor = Def(s.TitleColor, "#ffffff"),
                MessageColor = Def(s.MessageColor, "rgba(255, 255, 255, 0.72)"),
                BadgeTextColor = Def(s.BadgeTextColor, "#1a1a22"),
                BadgeBackgroundStart = Def(s.BadgeBackgroundStart, "#f4e5a1"),
                BadgeBackgroundEnd = Def(s.BadgeBackgroundEnd, "#d4af37"),
                TimerDigitColor = Def(s.TimerDigitColor, "#f4e5a1"),
                TimerLabelColor = Def(s.TimerLabelColor, "rgba(255, 255, 255, 0.45)"),
                TimerBoxBackground = Def(s.TimerBoxBackground, "rgba(212, 175, 55, 0.08)"),
                TimerBoxBorderColor = Def(s.TimerBoxBorderColor, "rgba(212, 175, 55, 0.35)"),
                CtaBackgroundStart = Def(s.CtaBackgroundStart, "#d4af37"),
                CtaBackgroundEnd = Def(s.CtaBackgroundEnd, "#f4e5a1"),
                CtaTextColor = Def(s.CtaTextColor, "#1a1a22"),
                FontFamily = string.IsNullOrWhiteSpace(s.FontFamily) ? "inherit" : s.FontFamily,

                TimerDigitFontSize = s.TimerDigitFontSize > 0 ? s.TimerDigitFontSize : 26,
                TimerLabelFontSize = s.TimerLabelFontSize > 0 ? s.TimerLabelFontSize : 9,
                TimerBoxMinWidth = s.TimerBoxMinWidth > 0 ? s.TimerBoxMinWidth : 58,
                TimerBoxBorderRadius = s.TimerBoxBorderRadius >= 0 ? s.TimerBoxBorderRadius : 10,
                TimerBoxPadding = s.TimerBoxPadding >= 0 ? s.TimerBoxPadding : 8,
                TimerGap = s.TimerGap >= 0 ? s.TimerGap : 10,
                CardBorderRadius = s.CardBorderRadius >= 0 ? s.CardBorderRadius : 16,
                CardPaddingTop = s.CardPaddingTop >= 0 ? s.CardPaddingTop : 28,
                CardPaddingSide = s.CardPaddingSide >= 0 ? s.CardPaddingSide : 24,
                CardPaddingBottom = s.CardPaddingBottom >= 0 ? s.CardPaddingBottom : 24,
                CardMarginBottom = s.CardMarginBottom >= 0 ? s.CardMarginBottom : 22,
                CardBorderWidth = s.CardBorderWidth >= 0 ? s.CardBorderWidth : 1,
                CardTextAlign = string.IsNullOrWhiteSpace(s.CardTextAlign) ? "center" : s.CardTextAlign,
                TitleFontSize = s.TitleFontSize > 0 ? s.TitleFontSize : 22,
                TitleFontWeight = s.TitleFontWeight > 0 ? s.TitleFontWeight : 700,
                MessageFontSize = s.MessageFontSize > 0 ? s.MessageFontSize : 14,
                BadgeFontSize = s.BadgeFontSize > 0 ? s.BadgeFontSize : 11,
                BadgeFontWeight = s.BadgeFontWeight > 0 ? s.BadgeFontWeight : 800,
                BadgeLetterSpacing = s.BadgeLetterSpacing >= 0 ? s.BadgeLetterSpacing : 2,
                BadgePaddingY = s.BadgePaddingY >= 0 ? s.BadgePaddingY : 5,
                BadgePaddingX = s.BadgePaddingX >= 0 ? s.BadgePaddingX : 14,
                CtaFontSize = s.CtaFontSize > 0 ? s.CtaFontSize : 14,
                CtaFontWeight = s.CtaFontWeight > 0 ? s.CtaFontWeight : 700,
                CtaPaddingY = s.CtaPaddingY >= 0 ? s.CtaPaddingY : 11,
                CtaPaddingX = s.CtaPaddingX >= 0 ? s.CtaPaddingX : 28,
                CtaBorderRadius = s.CtaBorderRadius >= 0 ? s.CtaBorderRadius : 999,

                EnableCartPopup = s.EnableCartPopup,
                PopupShowDelayMs = s.PopupShowDelayMs > 0 ? s.PopupShowDelayMs : 500,
                PopupOncePerSession = s.PopupOncePerSession,
                PopupAnimationType = s.PopupAnimationType,
                PopupAnimationDurationMs = s.PopupAnimationDurationMs > 0 ? s.PopupAnimationDurationMs : 350,
                PopupCloseOnOverlayClick = s.PopupCloseOnOverlayClick,
                PopupCloseOnEscape = s.PopupCloseOnEscape,
                PopupOverlayOpacity = s.PopupOverlayOpacity > 0 ? s.PopupOverlayOpacity : 72,
                PopupOverlayBlurPx = s.PopupOverlayBlurPx >= 0 ? s.PopupOverlayBlurPx : 6,
                PopupModalMaxWidth = s.PopupModalMaxWidth > 0 ? s.PopupModalMaxWidth : 420,
                PopupTitle = string.IsNullOrWhiteSpace(s.PopupTitle) ? "Non perderti l'edizione limitata!" : s.PopupTitle,
                PopupSubtitle = string.IsNullOrWhiteSpace(s.PopupSubtitle) ? "Questi prodotti esclusivi stanno per scadere" : s.PopupSubtitle,
                PopupContinueText = string.IsNullOrWhiteSpace(s.PopupContinueText) ? "Continua lo shopping" : s.PopupContinueText,
                PopupShowBadge = s.PopupShowBadge,
                PopupShowProductList = s.PopupShowProductList,
                PopupEnableGlow = s.PopupEnableGlow,
                PopupEnableSheen = s.PopupEnableSheen


            };
        }

        private static string Def(string value, string fallback)
            => string.IsNullOrWhiteSpace(value) ? fallback : value;
    }
}
