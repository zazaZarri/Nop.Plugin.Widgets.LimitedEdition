using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.LimitedEdition.Services;
using Nop.Services.Configuration;
using Nop.Core;
using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Controllers
{
    /// <summary>
    /// API pubbliche: countdown server-side, social proof feed, check purchase.
    /// </summary>
    [Route("LimitedEditionPublic")]
    public class LimitedEditionPublicController : Controller
    {
        private readonly ILimitedTimeProductService _ltpService;
        private readonly ILimitedEditionFeatureService _featureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public LimitedEditionPublicController(
            ILimitedTimeProductService ltpService,
            ILimitedEditionFeatureService featureService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _ltpService = ltpService;
            _featureService = featureService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        [HttpGet("Countdown")]
        public async Task<IActionResult> Countdown(int productId)
        {
            var ltp = await _ltpService.GetActiveByProductIdAsync(productId);
            if (ltp == null)
            {
                // prova comunque a leggere anche scaduti per secondi = 0
                var all = await _ltpService.GetAllPagedAsync(0, 50);
                ltp = all.FirstOrDefault(x => x.ProductId == productId);
            }

            if (ltp == null)
                return Json(new { success = false, secondsRemaining = 0 });

            var seconds = Math.Max(0, (int)(ltp.EndDateUtc - DateTime.UtcNow).TotalSeconds);
            return Json(new
            {
                success = true,
                productId,
                secondsRemaining = seconds,
                endDateUtc = ltp.EndDateUtc.ToString("o"),
                remainingQuantity = ltp.RemainingQuantity,
                soldCount = ltp.SoldCount,
                progressPercent = _featureService.ComputeProgressPercent(ltp)
            });
        }

        [HttpGet("SocialProofFeed")]
        public async Task<IActionResult> SocialProofFeed(int take = 8)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);
            if (!settings.EnableSocialProof)
                return Json(new { success = true, items = Array.Empty<object>() });

            var events = await _featureService.GetRecentSocialProofAsync(take);
            var items = events.Select(e => new
            {
                productId = e.ProductId,
                productName = e.ProductName,
                eventType = e.EventType,
                city = e.CityOrRegion,
                createdOnUtc = e.CreatedOnUtc.ToString("o"),
                minutesAgo = Math.Max(0, (int)(DateTime.UtcNow - e.CreatedOnUtc).TotalMinutes)
            }).ToList();

            // se pochi eventi e simulazione attiva, riempi
            if (settings.SocialProofIncludeSimulated && items.Count < 3)
            {
                var active = await _ltpService.GetAllPagedAsync(0, 10);
                var now = DateTime.UtcNow;
                foreach (var a in active.Where(x => x.IsActive && x.StartDateUtc <= now && x.EndDateUtc >= now).Take(5))
                {
                    items.Add(new
                    {
                        productId = a.ProductId,
                        productName = "Edizione limitata",
                        eventType = "viewing",
                        city = new[] { "Milano", "Roma", "Napoli", "Torino" }[a.ProductId % 4],
                        createdOnUtc = now.AddMinutes(-(a.ProductId % 15)).ToString("o"),
                        minutesAgo = a.ProductId % 15
                    });
                }
            }

            return Json(new { success = true, items });
        }

        [HttpGet("CanPurchase")]
        public async Task<IActionResult> CanPurchase(int productId)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);
            var ltp = await _ltpService.GetActiveByProductIdAsync(productId);

            // se non c'è edizione attiva, acquisto libero
            if (ltp == null)
            {
                var all = await _ltpService.GetAllPagedAsync(0, 100);
                var expired = all.FirstOrDefault(x => x.ProductId == productId && x.IsActive);
                if (expired != null && _featureService.ShouldBlockPurchase(expired, settings))
                    return Json(new { allowed = false, reason = "expired_or_soldout" });
                return Json(new { allowed = true });
            }

            var blocked = _featureService.ShouldBlockPurchase(ltp, settings);
            return Json(new
            {
                allowed = !blocked,
                reason = blocked ? (ltp.RemainingQuantity <= 0 && ltp.InitialQuantity > 0 ? "soldout" : "expired") : null,
                remainingQuantity = ltp.RemainingQuantity
            });
        }
    }
}
