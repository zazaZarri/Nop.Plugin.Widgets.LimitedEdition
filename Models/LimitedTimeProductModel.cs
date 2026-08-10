using System;
using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Modello di riga per la griglia admin (Tab 1) e per il form di editing.
    /// Non è l'entità di dominio: mappa i suoi campi più dati aggiuntivi
    /// per la sola visualizzazione (es. ProductName).
    /// </summary>
    public partial record LimitedTimeProductModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Fields.ProductId")]
        public int ProductId { get; set; }

        /// <summary>
        /// Nome del prodotto nativo, letto a runtime tramite IProductService
        /// SOLO per la visualizzazione in griglia (non salvato in questa tabella)
        /// </summary>
        public string ProductName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Fields.StartDateUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? StartDateUtc { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Fields.EndDateUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? EndDateUtc { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Fields.IsActive")]
        public bool IsActive { get; set; }
    }
}