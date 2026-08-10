using System.Collections.Generic;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Modello unico usato dalla view LimitedEditionView sia per il timer
    /// sulla pagina prodotto (un solo elemento, IsProductPage = true) sia
    /// per l'elenco offerte in homepage (più elementi, IsProductPage = false).
    /// </summary>
    public class HomepageOffersModel
    {
        public HomepageOffersModel()
        {
            Offers = new List<PublicInfoModel>();
        }

        /// <summary>
        /// True quando il widget è mostrato sulla pagina del singolo prodotto:
        /// in tal caso la view mostra solo il countdown, senza titolo/link/CTA.
        /// </summary>
        public bool IsProductPage { get; set; }

        public IList<PublicInfoModel> Offers { get; set; }
    }
}
