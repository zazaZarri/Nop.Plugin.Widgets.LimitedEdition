namespace Nop.Plugin.Widgets.LimitedEdition.Domain
{
    /// <summary>
    /// Template visuali disponibili per il popup carrello.
    /// Ogni template ha struttura e personalizzazioni indipendenti.
    /// </summary>
    public enum PopupTemplateType
    {
        /// <summary>Gold luxury dark (default attuale)</summary>
        Classic = 0,

        /// <summary>Chiaro, minimale, bordi sottili</summary>
        Minimal = 1,

        /// <summary>Scuro neon con glow</summary>
        Neon = 2,

        /// <summary>Pastel soft arrotondato</summary>
        Soft = 3
    }
}
