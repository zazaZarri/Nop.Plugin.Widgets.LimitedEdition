using System;
using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    public partial record LimitedTimeProductModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Fields.ProductId")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Fields.StartDateUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? StartDateUtc { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Fields.EndDateUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? EndDateUtc { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.LimitedEdition.Fields.IsActive")]
        public bool IsActive { get; set; }

        public int InitialQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public int SoldCount { get; set; }
        public bool ShowRemainingStock { get; set; }
        public bool ShowSoldCount { get; set; }
        public bool ShowProgressBar { get; set; }
        public int ProgressBarMode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool BlockPurchaseWhenExpired { get; set; }
    }
}
