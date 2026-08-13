using System;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.Widgets.LimitedEdition.Services
{
    /// <summary>
    /// Task schedulato: se EnableExpiryReminders, logga (e può estendersi a email)
    /// i clienti con prodotti limited in carrello in scadenza entro ReminderHoursBeforeExpiry.
    /// Registrare in Admin > System > Schedule tasks dopo install.
    /// </summary>
    public class LimitedEditionReminderTask : IScheduleTask
    {
        private readonly ISettingService _settingService;
        private readonly ILimitedTimeProductService _ltpService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;
        private readonly IRepository<Customer> _customerRepository;

        public LimitedEditionReminderTask(
            ISettingService settingService,
            ILimitedTimeProductService ltpService,
            IShoppingCartService shoppingCartService,
            ICustomerService customerService,
            ILogger logger,
            IRepository<Customer> customerRepository)
        {
            _settingService = settingService;
            _ltpService = ltpService;
            _shoppingCartService = shoppingCartService;
            _customerService = customerService;
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task ExecuteAsync()
        {
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>();
            if (!settings.EnableExpiryReminders)
                return;

            var hours = settings.ReminderHoursBeforeExpiry > 0 ? settings.ReminderHoursBeforeExpiry : 6;
            var now = DateTime.UtcNow;
            var windowEnd = now.AddHours(hours);

            var all = await _ltpService.GetAllPagedAsync(0, 200);
            var expiring = all.Where(x =>
                x.IsActive &&
                x.EndDateUtc > now &&
                x.EndDateUtc <= windowEnd).ToList();

            if (!expiring.Any())
                return;

            var productIds = expiring.Select(x => x.ProductId).ToHashSet();
            // campione clienti recenti (limitato per performance)
            var customers = await _customerRepository.Table
                .OrderByDescending(c => c.Id)
                .Take(500)
                .ToListAsync();

            var reminded = 0;
            foreach (var customer in customers)
            {
                try
                {
                    var cart = await _shoppingCartService.GetShoppingCartAsync(customer, ShoppingCartType.ShoppingCart);
                    var hit = cart.Any(ci => productIds.Contains(ci.ProductId));
                    if (!hit) continue;

                    // Qui si collegherebbe IWorkflowMessageService / email personalizzata.
                    // Per non dipendere da template email custom non standard, logghiamo l'intent.
                    await _logger.InformationAsync(
                        $"LimitedEdition reminder: customer {customer.Id} ({customer.Email}) ha in carrello prodotti in scadenza entro {hours}h.");
                    reminded++;
                }
                catch
                {
                    // ignore single customer errors
                }
            }

            if (reminded > 0)
                await _logger.InformationAsync($"LimitedEdition reminder task: {reminded} clienti notificati (log).");
        }
    }
}
