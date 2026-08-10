using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Services.Events;

public class EventConsumer : IConsumer<EntityInsertedEvent<ShoppingCartItem>>
{
    private readonly IRepository<CustomerTable> _customerActionRepository;

    public EventConsumer(IRepository<CustomerTable> customerActionRepository)
    {
        _customerActionRepository = customerActionRepository;
    }

    public async Task HandleEventAsync(EntityInsertedEvent<ShoppingCartItem> eventMessage)
    {
        var cartItem = eventMessage?.Entity;
        if (cartItem == null)
            return;

        var customerId = cartItem.CustomerId;

        var record = await _customerActionRepository.Table.FirstOrDefaultAsync(x => x.CustomerId == customerId);

        if (record == null)
        {
            record = new CustomerTable
            {
                CustomerId = customerId,
                IsEnabled = true
            };
            await _customerActionRepository.InsertAsync(record);
        }
        else
        {
            record.IsEnabled = true;
            await _customerActionRepository.UpdateAsync(record);
        }
    }

}
