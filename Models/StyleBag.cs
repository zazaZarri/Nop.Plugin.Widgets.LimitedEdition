using System;
using System.Collections.Generic;
using System.Text.Json;
using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Bag di stile serializzabile per un singolo template (card o popup).
    /// Contiene solo proprietà visuali; testi globali restano in LimitedTimeSettings.
    /// </summary>
    public class StyleBag
    {
        // Testi (override opzionali per template; se null usano i globali)
        public string BadgeText { get; set; }
        public string CtaText { get; set; }
        public string ExpiredText { get; set; }
        public string DaysLabel { get; set; }
        public string HoursLabel { get; set; }
        public string MinutesLabel { get; set; }
        public string SecondsLabel { get; set; }

        // Visibilità
        public bool ShowBadge { get; set; } = true;
        public bool ShowTitle { get; set; } = true;
        public bool ShowMessage { get; set; } = true;
        public bool ShowCta { get; set; } = true;
        public bool EnableGlowAnimation { get; set; } = true;
        public bool EnableSheenAnimation { get; set; } = true;
        public int TimerLayoutId { get; set; }

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

        // Popup-specific visual (oltre al bag comune)
        public int PopupOverlayOpacity { get; set; }
        public int PopupOverlayBlurPx { get; set; }
        public int PopupModalMaxWidth { get; set; }
        public int PopupAnimationType { get; set; }
        public int PopupAnimationDurationMs { get; set; }
        public bool PopupShowBadge { get; set; } = true;
        public bool PopupShowProductList { get; set; } = true;
        public bool PopupEnableGlow { get; set; } = true;
        public bool PopupEnableSheen { get; set; }
        public string PopupTitle { get; set; }
        public string PopupSubtitle { get; set; }
        public string PopupContinueText { get; set; }

        // ── Defaults per template ──────────────────────────────────────────

        public static StyleBag CreateDefault(CardTemplateType template)
        {
            return template switch
            {
                CardTemplateType.Minimal => CreateMinimalCard(),
                CardTemplateType.Neon => CreateNeonCard(),
                CardTemplateType.Soft => CreateSoftCard(),
                CardTemplateType.Story => CreateStoryCard(),
                _ => CreateClassicCard()
            };
        }

        public static StyleBag CreateDefault(PopupTemplateType template)
        {
            return template switch
            {
                PopupTemplateType.Minimal => CreateMinimalPopup(),
                PopupTemplateType.Neon => CreateNeonPopup(),
                PopupTemplateType.Soft => CreateSoftPopup(),
                _ => CreateClassicPopup()
            };
        }

        public static StyleBag CreateClassicCard()
        {
            return new StyleBag
            {
                BadgeText = "⚡ Edizione Limitata",
                CtaText = "Vedi offerta →",
                ExpiredText = "Offerta scaduta!",
                DaysLabel = "Giorni",
                HoursLabel = "Ore",
                MinutesLabel = "Min",
                SecondsLabel = "Sec",
                ShowBadge = true,
                ShowTitle = true,
                ShowMessage = true,
                ShowCta = true,
                EnableGlowAnimation = true,
                EnableSheenAnimation = true,
                TimerLayoutId = 0,
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
                CardTextAlign = "center",
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
                FontFamily = "inherit"
            };
        }

        public static StyleBag CreateMinimalCard()
        {
            return new StyleBag
            {
                BadgeText = "LIMITED",
                CtaText = "Scopri",
                ExpiredText = "Scaduto",
                DaysLabel = "GG",
                HoursLabel = "HH",
                MinutesLabel = "MM",
                SecondsLabel = "SS",
                ShowBadge = true,
                ShowTitle = true,
                ShowMessage = true,
                ShowCta = true,
                EnableGlowAnimation = false,
                EnableSheenAnimation = false,
                TimerLayoutId = 2, // Minimal
                AccentColor = "#111111",
                AccentColorLight = "#666666",
                CardBackgroundStart = "#ffffff",
                CardBackgroundMid = "#fafafa",
                CardBackgroundEnd = "#ffffff",
                BorderColor = "#e5e5e5",
                TitleColor = "#111111",
                MessageColor = "#666666",
                BadgeTextColor = "#ffffff",
                BadgeBackgroundStart = "#111111",
                BadgeBackgroundEnd = "#333333",
                TimerDigitColor = "#111111",
                TimerLabelColor = "#999999",
                TimerBoxBackground = "transparent",
                TimerBoxBorderColor = "transparent",
                CtaBackgroundStart = "#111111",
                CtaBackgroundEnd = "#333333",
                CtaTextColor = "#ffffff",
                TimerDigitFontSize = 28,
                TimerLabelFontSize = 8,
                TimerBoxMinWidth = 48,
                TimerBoxBorderRadius = 0,
                TimerBoxPadding = 4,
                TimerGap = 16,
                CardBorderRadius = 4,
                CardPaddingTop = 24,
                CardPaddingSide = 20,
                CardPaddingBottom = 20,
                CardMarginBottom = 16,
                CardBorderWidth = 1,
                CardTextAlign = "left",
                TitleFontSize = 18,
                TitleFontWeight = 600,
                MessageFontSize = 13,
                BadgeFontSize = 10,
                BadgeFontWeight = 700,
                BadgeLetterSpacing = 3,
                BadgePaddingY = 4,
                BadgePaddingX = 10,
                CtaFontSize = 13,
                CtaFontWeight = 600,
                CtaPaddingY = 10,
                CtaPaddingX = 24,
                CtaBorderRadius = 2,
                FontFamily = "'Inter', sans-serif"
            };
        }

        public static StyleBag CreateNeonCard()
        {
            return new StyleBag
            {
                BadgeText = "◆ LIMITED DROP",
                CtaText = "GET IT NOW →",
                ExpiredText = "SOLD OUT",
                DaysLabel = "DAYS",
                HoursLabel = "HRS",
                MinutesLabel = "MIN",
                SecondsLabel = "SEC",
                ShowBadge = true,
                ShowTitle = true,
                ShowMessage = true,
                ShowCta = true,
                EnableGlowAnimation = true,
                EnableSheenAnimation = true,
                TimerLayoutId = 1, // Boxed
                AccentColor = "#00f0ff",
                AccentColorLight = "#ff00aa",
                CardBackgroundStart = "#050510",
                CardBackgroundMid = "#0a0a1a",
                CardBackgroundEnd = "#050510",
                BorderColor = "rgba(0, 240, 255, 0.5)",
                TitleColor = "#ffffff",
                MessageColor = "rgba(0, 240, 255, 0.75)",
                BadgeTextColor = "#050510",
                BadgeBackgroundStart = "#00f0ff",
                BadgeBackgroundEnd = "#ff00aa",
                TimerDigitColor = "#00f0ff",
                TimerLabelColor = "rgba(255, 0, 170, 0.7)",
                TimerBoxBackground = "rgba(0, 240, 255, 0.06)",
                TimerBoxBorderColor = "rgba(0, 240, 255, 0.4)",
                CtaBackgroundStart = "#00f0ff",
                CtaBackgroundEnd = "#ff00aa",
                CtaTextColor = "#050510",
                TimerDigitFontSize = 28,
                TimerLabelFontSize = 8,
                TimerBoxMinWidth = 60,
                TimerBoxBorderRadius = 2,
                TimerBoxPadding = 10,
                TimerGap = 8,
                CardBorderRadius = 4,
                CardPaddingTop = 26,
                CardPaddingSide = 22,
                CardPaddingBottom = 22,
                CardMarginBottom = 20,
                CardBorderWidth = 2,
                CardTextAlign = "center",
                TitleFontSize = 20,
                TitleFontWeight = 800,
                MessageFontSize = 13,
                BadgeFontSize = 10,
                BadgeFontWeight = 800,
                BadgeLetterSpacing = 2,
                BadgePaddingY = 5,
                BadgePaddingX = 12,
                CtaFontSize = 13,
                CtaFontWeight = 800,
                CtaPaddingY = 12,
                CtaPaddingX = 26,
                CtaBorderRadius = 2,
                FontFamily = "'Space Grotesk', sans-serif"
            };
        }

        public static StyleBag CreateSoftCard()
        {
            return new StyleBag
            {
                BadgeText = "✨ Edizione speciale",
                CtaText = "Scopri di più",
                ExpiredText = "Offerta terminata",
                DaysLabel = "Giorni",
                HoursLabel = "Ore",
                MinutesLabel = "Min",
                SecondsLabel = "Sec",
                ShowBadge = true,
                ShowTitle = true,
                ShowMessage = true,
                ShowCta = true,
                EnableGlowAnimation = false,
                EnableSheenAnimation = false,
                TimerLayoutId = 0,
                AccentColor = "#c9a0a0",
                AccentColorLight = "#e8d0d0",
                CardBackgroundStart = "#fdf8f6",
                CardBackgroundMid = "#f9f0ee",
                CardBackgroundEnd = "#fdf8f6",
                BorderColor = "rgba(201, 160, 160, 0.35)",
                TitleColor = "#4a3a3a",
                MessageColor = "#7a6a6a",
                BadgeTextColor = "#ffffff",
                BadgeBackgroundStart = "#c9a0a0",
                BadgeBackgroundEnd = "#b08080",
                TimerDigitColor = "#8a6060",
                TimerLabelColor = "#b0a0a0",
                TimerBoxBackground = "rgba(201, 160, 160, 0.12)",
                TimerBoxBorderColor = "rgba(201, 160, 160, 0.25)",
                CtaBackgroundStart = "#c9a0a0",
                CtaBackgroundEnd = "#e8d0d0",
                CtaTextColor = "#4a3a3a",
                TimerDigitFontSize = 24,
                TimerLabelFontSize = 9,
                TimerBoxMinWidth = 56,
                TimerBoxBorderRadius = 16,
                TimerBoxPadding = 10,
                TimerGap = 12,
                CardBorderRadius = 24,
                CardPaddingTop = 32,
                CardPaddingSide = 28,
                CardPaddingBottom = 28,
                CardMarginBottom = 24,
                CardBorderWidth = 1,
                CardTextAlign = "center",
                TitleFontSize = 20,
                TitleFontWeight = 600,
                MessageFontSize = 14,
                BadgeFontSize = 11,
                BadgeFontWeight = 600,
                BadgeLetterSpacing = 1,
                BadgePaddingY = 6,
                BadgePaddingX = 16,
                CtaFontSize = 14,
                CtaFontWeight = 600,
                CtaPaddingY = 12,
                CtaPaddingX = 28,
                CtaBorderRadius = 999,
                FontFamily = "'Playfair Display', serif"
            };
        }


        public static StyleBag CreateStoryCard()
        {
            return new StyleBag
            {
                BadgeText = "LIMITED DROP",
                CtaText = "Scopri ora →",
                ExpiredText = "Drop terminato",
                DaysLabel = "Giorni",
                HoursLabel = "Ore",
                MinutesLabel = "Min",
                SecondsLabel = "Sec",
                ShowBadge = true,
                ShowTitle = true,
                ShowMessage = true,
                ShowCta = true,
                EnableGlowAnimation = true,
                EnableSheenAnimation = false,
                TimerLayoutId = 0,
                AccentColor = "#ffffff",
                AccentColorLight = "#f0f0f0",
                CardBackgroundStart = "#111111",
                CardBackgroundMid = "#1a1a1a",
                CardBackgroundEnd = "#0a0a0a",
                BorderColor = "rgba(255,255,255,0.15)",
                TitleColor = "#ffffff",
                MessageColor = "rgba(255,255,255,0.85)",
                BadgeTextColor = "#111111",
                BadgeBackgroundStart = "#ffffff",
                BadgeBackgroundEnd = "#e0e0e0",
                TimerDigitColor = "#ffffff",
                TimerLabelColor = "rgba(255,255,255,0.55)",
                TimerBoxBackground = "rgba(255,255,255,0.12)",
                TimerBoxBorderColor = "rgba(255,255,255,0.25)",
                CtaBackgroundStart = "#ffffff",
                CtaBackgroundEnd = "#f5f5f5",
                CtaTextColor = "#111111",
                TimerDigitFontSize = 32,
                TimerLabelFontSize = 10,
                TimerBoxMinWidth = 64,
                TimerBoxBorderRadius = 12,
                TimerBoxPadding = 12,
                TimerGap = 12,
                CardBorderRadius = 20,
                CardPaddingTop = 48,
                CardPaddingSide = 32,
                CardPaddingBottom = 40,
                CardMarginBottom = 28,
                CardBorderWidth = 0,
                CardTextAlign = "center",
                TitleFontSize = 28,
                TitleFontWeight = 800,
                MessageFontSize = 16,
                BadgeFontSize = 11,
                BadgeFontWeight = 800,
                BadgeLetterSpacing = 3,
                BadgePaddingY = 6,
                BadgePaddingX = 16,
                CtaFontSize = 15,
                CtaFontWeight = 700,
                CtaPaddingY = 14,
                CtaPaddingX = 32,
                CtaBorderRadius = 999,
                FontFamily = "'Montserrat', sans-serif"
            };
        }

        public static StyleBag CreateClassicPopup()
        {
            var s = CreateClassicCard();
            s.PopupOverlayOpacity = 72;
            s.PopupOverlayBlurPx = 6;
            s.PopupModalMaxWidth = 420;
            s.PopupAnimationType = 1;
            s.PopupAnimationDurationMs = 350;
            s.PopupShowBadge = true;
            s.PopupShowProductList = true;
            s.PopupEnableGlow = true;
            s.PopupEnableSheen = false;
            s.PopupTitle = "Non perderti l'edizione limitata!";
            s.PopupSubtitle = "Questi prodotti esclusivi stanno per scadere";
            s.PopupContinueText = "Continua lo shopping";
            return s;
        }

        public static StyleBag CreateMinimalPopup()
        {
            var s = CreateMinimalCard();
            s.PopupOverlayOpacity = 40;
            s.PopupOverlayBlurPx = 2;
            s.PopupModalMaxWidth = 380;
            s.PopupAnimationType = 0;
            s.PopupAnimationDurationMs = 250;
            s.PopupShowBadge = true;
            s.PopupShowProductList = true;
            s.PopupEnableGlow = false;
            s.PopupEnableSheen = false;
            s.PopupTitle = "Edizioni limitate in scadenza";
            s.PopupSubtitle = "Alcuni prodotti esclusivi stanno per esaurirsi";
            s.PopupContinueText = "Continua";
            return s;
        }

        public static StyleBag CreateNeonPopup()
        {
            var s = CreateNeonCard();
            s.PopupOverlayOpacity = 80;
            s.PopupOverlayBlurPx = 10;
            s.PopupModalMaxWidth = 440;
            s.PopupAnimationType = 2;
            s.PopupAnimationDurationMs = 400;
            s.PopupShowBadge = true;
            s.PopupShowProductList = true;
            s.PopupEnableGlow = true;
            s.PopupEnableSheen = true;
            s.PopupTitle = "LIMITED DROPS EXPIRING";
            s.PopupSubtitle = "Non perdere questi pezzi unici";
            s.PopupContinueText = "KEEP SHOPPING";
            return s;
        }

        public static StyleBag CreateSoftPopup()
        {
            var s = CreateSoftCard();
            s.PopupOverlayOpacity = 50;
            s.PopupOverlayBlurPx = 8;
            s.PopupModalMaxWidth = 400;
            s.PopupAnimationType = 1;
            s.PopupAnimationDurationMs = 400;
            s.PopupShowBadge = true;
            s.PopupShowProductList = true;
            s.PopupEnableGlow = false;
            s.PopupEnableSheen = false;
            s.PopupTitle = "Edizioni speciali in scadenza";
            s.PopupSubtitle = "Prodotti selezionati stanno per terminare";
            s.PopupContinueText = "Continua lo shopping";
            return s;
        }

        // ── Serialization helpers ──────────────────────────────────────────

        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };

        public static string SerializeDict(Dictionary<int, StyleBag> dict)
        {
            if (dict == null || dict.Count == 0)
                return "{}";
            return JsonSerializer.Serialize(dict, JsonOpts);
        }

        public static Dictionary<int, StyleBag> DeserializeDict(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new Dictionary<int, StyleBag>();
            try
            {
                return JsonSerializer.Deserialize<Dictionary<int, StyleBag>>(json, JsonOpts)
                       ?? new Dictionary<int, StyleBag>();
            }
            catch
            {
                return new Dictionary<int, StyleBag>();
            }
        }

        public static Dictionary<int, StyleBag> EnsureCardTemplates(string json)
        {
            var dict = DeserializeDict(json);
            foreach (CardTemplateType t in Enum.GetValues(typeof(CardTemplateType)))
            {
                var id = (int)t;
                if (!dict.ContainsKey(id))
                    dict[id] = CreateDefault(t);
            }
            return dict;
        }

        public static Dictionary<int, StyleBag> EnsurePopupTemplates(string json)
        {
            var dict = DeserializeDict(json);
            foreach (PopupTemplateType t in Enum.GetValues(typeof(PopupTemplateType)))
            {
                var id = (int)t;
                if (!dict.ContainsKey(id))
                    dict[id] = CreateDefault(t);
            }
            return dict;
        }

        /// <summary>
        /// Converte StyleBag nel modello usato dalle view pubbliche.
        /// </summary>
        public StyleSettingsModel ToStyleSettingsModel(LimitedTimeSettings global = null)
        {
            return new StyleSettingsModel
            {
                BadgeText = !string.IsNullOrWhiteSpace(BadgeText) ? BadgeText : (global?.BadgeText ?? "⚡ Edizione Limitata"),
                CtaText = !string.IsNullOrWhiteSpace(CtaText) ? CtaText : (global?.CtaText ?? "Vedi offerta →"),
                ExpiredText = !string.IsNullOrWhiteSpace(ExpiredText) ? ExpiredText : (global?.ExpiredText ?? "Offerta scaduta!"),
                DaysLabel = !string.IsNullOrWhiteSpace(DaysLabel) ? DaysLabel : (global?.DaysLabel ?? "Giorni"),
                HoursLabel = !string.IsNullOrWhiteSpace(HoursLabel) ? HoursLabel : (global?.HoursLabel ?? "Ore"),
                MinutesLabel = !string.IsNullOrWhiteSpace(MinutesLabel) ? MinutesLabel : (global?.MinutesLabel ?? "Min"),
                SecondsLabel = !string.IsNullOrWhiteSpace(SecondsLabel) ? SecondsLabel : (global?.SecondsLabel ?? "Sec"),

                ShowBadge = ShowBadge,
                ShowTitle = ShowTitle,
                ShowMessage = ShowMessage,
                ShowCta = ShowCta,
                EnableGlowAnimation = EnableGlowAnimation,
                EnableSheenAnimation = EnableSheenAnimation,
                TimerLayout = (TimerLayoutType)TimerLayoutId,

                AccentColor = Def(AccentColor, "#d4af37"),
                AccentColorLight = Def(AccentColorLight, "#f4e5a1"),
                CardBackgroundStart = Def(CardBackgroundStart, "#0b0b0f"),
                CardBackgroundMid = Def(CardBackgroundMid, "#1a1a22"),
                CardBackgroundEnd = Def(CardBackgroundEnd, "#0b0b0f"),
                BorderColor = Def(BorderColor, "rgba(212, 175, 55, 0.45)"),
                TitleColor = Def(TitleColor, "#ffffff"),
                MessageColor = Def(MessageColor, "rgba(255, 255, 255, 0.72)"),
                BadgeTextColor = Def(BadgeTextColor, "#1a1a22"),
                BadgeBackgroundStart = Def(BadgeBackgroundStart, "#f4e5a1"),
                BadgeBackgroundEnd = Def(BadgeBackgroundEnd, "#d4af37"),
                TimerDigitColor = Def(TimerDigitColor, "#f4e5a1"),
                TimerLabelColor = Def(TimerLabelColor, "rgba(255, 255, 255, 0.45)"),
                TimerBoxBackground = Def(TimerBoxBackground, "rgba(212, 175, 55, 0.08)"),
                TimerBoxBorderColor = Def(TimerBoxBorderColor, "rgba(212, 175, 55, 0.35)"),
                CtaBackgroundStart = Def(CtaBackgroundStart, "#d4af37"),
                CtaBackgroundEnd = Def(CtaBackgroundEnd, "#f4e5a1"),
                CtaTextColor = Def(CtaTextColor, "#1a1a22"),
                FontFamily = string.IsNullOrWhiteSpace(FontFamily) ? "inherit" : FontFamily,

                TimerDigitFontSize = TimerDigitFontSize > 0 ? TimerDigitFontSize : 26,
                TimerLabelFontSize = TimerLabelFontSize > 0 ? TimerLabelFontSize : 9,
                TimerBoxMinWidth = TimerBoxMinWidth > 0 ? TimerBoxMinWidth : 58,
                TimerBoxBorderRadius = TimerBoxBorderRadius >= 0 ? TimerBoxBorderRadius : 10,
                TimerBoxPadding = TimerBoxPadding >= 0 ? TimerBoxPadding : 8,
                TimerGap = TimerGap >= 0 ? TimerGap : 10,
                CardBorderRadius = CardBorderRadius >= 0 ? CardBorderRadius : 16,
                CardPaddingTop = CardPaddingTop >= 0 ? CardPaddingTop : 28,
                CardPaddingSide = CardPaddingSide >= 0 ? CardPaddingSide : 24,
                CardPaddingBottom = CardPaddingBottom >= 0 ? CardPaddingBottom : 24,
                CardMarginBottom = CardMarginBottom >= 0 ? CardMarginBottom : 22,
                CardBorderWidth = CardBorderWidth >= 0 ? CardBorderWidth : 1,
                CardTextAlign = string.IsNullOrWhiteSpace(CardTextAlign) ? "center" : CardTextAlign,
                TitleFontSize = TitleFontSize > 0 ? TitleFontSize : 22,
                TitleFontWeight = TitleFontWeight > 0 ? TitleFontWeight : 700,
                MessageFontSize = MessageFontSize > 0 ? MessageFontSize : 14,
                BadgeFontSize = BadgeFontSize > 0 ? BadgeFontSize : 11,
                BadgeFontWeight = BadgeFontWeight > 0 ? BadgeFontWeight : 800,
                BadgeLetterSpacing = BadgeLetterSpacing >= 0 ? BadgeLetterSpacing : 2,
                BadgePaddingY = BadgePaddingY >= 0 ? BadgePaddingY : 5,
                BadgePaddingX = BadgePaddingX >= 0 ? BadgePaddingX : 14,
                CtaFontSize = CtaFontSize > 0 ? CtaFontSize : 14,
                CtaFontWeight = CtaFontWeight > 0 ? CtaFontWeight : 700,
                CtaPaddingY = CtaPaddingY >= 0 ? CtaPaddingY : 11,
                CtaPaddingX = CtaPaddingX >= 0 ? CtaPaddingX : 28,
                CtaBorderRadius = CtaBorderRadius >= 0 ? CtaBorderRadius : 999,

                EnableCartPopup = global?.EnableCartPopup ?? false,
                PopupShowDelayMs = global?.PopupShowDelayMs > 0 ? global.PopupShowDelayMs : 500,
                PopupOncePerSession = global?.PopupOncePerSession ?? false,
                PopupAnimationType = PopupAnimationType,
                PopupAnimationDurationMs = PopupAnimationDurationMs > 0 ? PopupAnimationDurationMs : 350,
                PopupCloseOnOverlayClick = global?.PopupCloseOnOverlayClick ?? true,
                PopupCloseOnEscape = global?.PopupCloseOnEscape ?? true,
                PopupOverlayOpacity = PopupOverlayOpacity > 0 ? PopupOverlayOpacity : 72,
                PopupOverlayBlurPx = PopupOverlayBlurPx >= 0 ? PopupOverlayBlurPx : 6,
                PopupModalMaxWidth = PopupModalMaxWidth > 0 ? PopupModalMaxWidth : 420,
                PopupTitle = !string.IsNullOrWhiteSpace(PopupTitle) ? PopupTitle : "Non perderti l'edizione limitata!",
                PopupSubtitle = !string.IsNullOrWhiteSpace(PopupSubtitle) ? PopupSubtitle : "Questi prodotti esclusivi stanno per scadere",
                PopupContinueText = !string.IsNullOrWhiteSpace(PopupContinueText) ? PopupContinueText : "Continua lo shopping",
                PopupShowBadge = PopupShowBadge,
                PopupShowProductList = PopupShowProductList,
                PopupEnableGlow = PopupEnableGlow,
                PopupEnableSheen = PopupEnableSheen,

                CardTemplateId = 0,
                PopupTemplateId = 0
            };
        }

        private static string Def(string value, string fallback)
            => string.IsNullOrWhiteSpace(value) ? fallback : value;
    }
}
