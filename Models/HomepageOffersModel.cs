using System.Collections.Generic;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Modello usato dalla view LimitedEditionView sia per il timer
    /// sulla pagina prodotto sia per l'elenco offerte in homepage.
    /// Include lo Style completo per CSS variables.
    /// </summary>
    public class HomepageOffersModel
    {
        public HomepageOffersModel()
        {
            Offers = new List<PublicInfoModel>();
        }

        public bool IsProductPage { get; set; }

        public IList<PublicInfoModel> Offers { get; set; }

        /// <summary>
        /// Stile globale del widget (stesso per tutte le card della pagina).
        /// </summary>
        public StyleSettingsModel Style { get; set; }
    }
}
