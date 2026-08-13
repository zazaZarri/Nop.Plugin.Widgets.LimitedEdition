using System;
using Nop.Core;

namespace Nop.Plugin.Widgets.LimitedEdition.Domain
{
    public class LimitedTimeProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }
        public bool IsActive { get; set; }

        public int InitialQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public int SoldCount { get; set; }
        public bool ShowRemainingStock { get; set; }
        public bool ShowSoldCount { get; set; }
        public bool ShowProgressBar { get; set; }
        /// <summary>0 = tempo, 1 = stock venduto</summary>
        public int ProgressBarMode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool BlockPurchaseWhenExpired { get; set; } = true;
    }
}
