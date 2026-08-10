using System;
using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    public class PublicInfoModel
    {
        public int ProductId { get; set; }

        public DateTime EndDateUtc { get; set; }

        public string CustomMessage { get; set; }

        public string BackgroundColor { get; set; }

        public string TextColor { get; set; }

        public TimerLayoutType TimerLayout { get; set; }

        /// <summary>
        /// Nome del prodotto. Valorizzato solo per le voci mostrate nella lista
        /// in homepage (non serve nella vista sulla pagina del singolo prodotto).
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// URL assoluto del prodotto. Valorizzato solo per le voci in homepage.
        /// </summary>
        public string ProductUrl { get; set; }
    }
}
