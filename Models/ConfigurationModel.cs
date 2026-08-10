using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Modello "contenitore" della pagina di configurazione con le 2 tab:
    /// Tab 1 = griglia prodotti (via SearchModel), Tab 2 = settings grafici/testuali.
    /// </summary>
    public partial record ConfigurationModel : BaseNopModel
    {
        public ConfigurationModel()
        {
            LimitedTimeProductSearchModel = new LimitedTimeProductSearchModel();
        }

        /// <summary>
        /// Modello di ricerca per la griglia della Tab 1
        /// </summary>
        public LimitedTimeProductSearchModel LimitedTimeProductSearchModel { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.BackgroundColor")]
        public string BackgroundColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TextColor")]
        public string TextColor { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.CustomMessage")]
        public string CustomMessage { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.TimerLayout")]
        public int TimerLayoutId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Settings.HideProductWhenExpired")]
        public bool HideProductWhenExpired { get; set; }

        public bool BackgroundColor_OverrideForStore { get; set; }
        public bool TextColor_OverrideForStore { get; set; }
        public bool CustomMessage_OverrideForStore { get; set; }
        public bool TimerLayoutId_OverrideForStore { get; set; }
        public bool HideProductWhenExpired_OverrideForStore { get; set; }

        public int ActiveStoreScopeConfiguration { get; set; }
    }
}