using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Plugin.Widgets.LimitedEdition.Services;
using Nop.Services.Events;
using Nop.Services.Orders;

namespace Nop.Plugin.Widgets.LimitedEdition.Consumers
{
    /// <summary>
    /// Decrementa stock edizione e registra social proof alla conferma ordine.
    /// </summary>
    public class OrderPaidConsumer : IConsumer<OrderPaidEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ILimitedEditionFeatureService _featureService;

        public OrderPaidConsumer(IOrderService orderService, ILimitedEditionFeatureService featureService)
        {
            _orderService = orderService;
            _featureService = featureService;
        }

        public async Task HandleEventAsync(OrderPaidEvent eventMessage)
        {
            var order = eventMessage?.Order;
            if (order == null)
                return;

            var items = await _orderService.GetOrderItemsAsync(order.Id);
            foreach (var item in items)
            {
                await _featureService.ApplyOrderSoldAsync(item.ProductId, item.Quantity);
                // city opzionale: FeatureService usa città simulate se null
                await _featureService.RecordSocialProofAsync(
                    item.ProductId,
                    "Prodotto",
                    "purchase",
                    city: null);
            }
        }
    }
}
