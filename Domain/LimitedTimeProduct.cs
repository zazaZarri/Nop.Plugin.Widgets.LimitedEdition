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
    }
}