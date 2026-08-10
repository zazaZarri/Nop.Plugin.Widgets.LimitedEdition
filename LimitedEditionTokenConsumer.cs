using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Services.Events;
using Nop.Services.Messages;

namespace Nop.Plugin.Widgets.LimitedEdition.Consumers
{
    public class LimitedEditionOrderTokenConsumer : IConsumer<EntityTokensAddedEvent<Order>>
    {
        public async Task HandleEventAsync(EntityTokensAddedEvent<Order> eventMessage)
        {

            var order = eventMessage.Entity;

            string valorePersonalizzato = "Ciao123";

            eventMessage.Tokens.Add(new Token("Order.MioTokenPersonalizzato", valorePersonalizzato));

            await Task.CompletedTask;
        }
    }
}