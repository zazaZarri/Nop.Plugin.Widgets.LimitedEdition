using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Modello di ricerca per la griglia Kendo (Tab 1).
    /// Eredita da BaseSearchModel per i campi di paginazione standard.
    /// </summary>
    public partial record LimitedTimeProductSearchModel : BaseSearchModel
    {
        public LimitedTimeProductSearchModel()
        {
            // Costruttore vuoto, pronto per eventuali futuri filtri di ricerca
        }
    }
}