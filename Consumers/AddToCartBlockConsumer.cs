using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Plugin.Widgets.LimitedEdition.Services;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Events;
using Nop.Services.Localization;

namespace Nop.Plugin.Widgets.LimitedEdition.Consumers
{
    /// <summary>
    /// Dopo insert in carrello: social proof + se bloccato non può fare molto post-insert,
    /// il blocco vero è lato API pubblica CheckPurchaseAllowed.
    /// </summary>
    public class AddToCartSocialConsumer : IConsumer<EntityInsertedEvent<ShoppingCartItem>>
    {
        private readonly ILimitedEditionFeatureService _featureService;
        private readonly IProductService _productService;
        private readonly ILimitedTimeProductService _ltpService;

        public AddToCartSocialConsumer(
            ILimitedEditionFeatureService featureService,
            IProductService productService,
            ILimitedTimeProductService ltpService)
        {
            _featureService = featureService;
            _productService = productService;
            _ltpService = ltpService;
        }

        public async Task HandleEventAsync(EntityInsertedEvent<ShoppingCartItem> eventMessage)
        {
            var item = eventMessage?.Entity;
            if (item == null) return;

            var product = await _productService.GetProductByIdAsync(item.ProductId);
            await _featureService.RecordSocialProofAsync(
                item.ProductId,
                product?.Name ?? "Prodotto",
                "add_to_cart");
        }
    }
}
