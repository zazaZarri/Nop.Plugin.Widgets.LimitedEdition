using Nop.Core;

namespace Nop.Plugin.Widgets.LimitedEdition.Domain
{
    /// <summary>
    /// Flag per cliente: abilita popup carrello dopo aggiunta prodotto.
    /// </summary>
    public class CustomerTable : BaseEntity
    {
        public int CustomerId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
