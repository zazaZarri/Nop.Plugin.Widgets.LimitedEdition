using System;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    public class PublicInfoModel
    {
        public int ProductId { get; set; }
        public DateTime EndDateUtc { get; set; }
        public DateTime StartDateUtc { get; set; }
        public string CustomMessage { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ProductImageUrl { get; set; }
        public StyleSettingsModel Style { get; set; }

        // Scarsità
        public int InitialQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public int SoldCount { get; set; }
        public bool ShowRemainingStock { get; set; }
        public bool ShowSoldCount { get; set; }
        public bool ShowProgressBar { get; set; }
        public int ProgressBarMode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsExpired { get; set; }
        public bool IsSoldOut { get; set; }
        public bool BlockPurchase { get; set; }

        // Badge dinamico calcolato
        public string DynamicBadgeText { get; set; }
        public bool UseDynamicBadge { get; set; }

        // Progress 0-100
        public double ProgressPercent { get; set; }
    }
}
