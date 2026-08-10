using Nop.Core;

namespace Nop.Plugin.Widgets.LimitedEdition.Domain
{
    public class CustomerTable : BaseEntity
    {
        public int CustomerId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
