using System;
using Nop.Core;

namespace Nop.Plugin.Widgets.LimitedEdition.Domain
{
    /// <summary>
    /// Evento per toast "qualcuno ha appena aggiunto/comprato".
    /// </summary>
    public class SocialProofEvent : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        /// <summary>add_to_cart | purchase | viewing</summary>
        public string EventType { get; set; }
        public string CityOrRegion { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
