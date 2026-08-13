using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Plugin.Widgets.LimitedEdition.Models;

namespace Nop.Plugin.Widgets.LimitedEdition.Services
{
    public class LimitedEditionFeatureService : ILimitedEditionFeatureService
    {
        private readonly IRepository<SocialProofEvent> _socialRepo;
        private readonly IRepository<LimitedTimeProduct> _ltpRepo;
        private static readonly string[] SimulatedCities =
            { "Milano", "Roma", "Torino", "Napoli", "Bologna", "Firenze", "Verona", "Palermo", "Genova", "Bari" };
        private static readonly Random Rnd = new();

        public LimitedEditionFeatureService(
            IRepository<SocialProofEvent> socialRepo,
            IRepository<LimitedTimeProduct> ltpRepo)
        {
            _socialRepo = socialRepo;
            _ltpRepo = ltpRepo;
        }

        public string ComputeDynamicBadge(LimitedTimeProduct ltp, LimitedTimeSettings settings)
        {
            if (settings == null || !settings.EnableDynamicBadges || ltp == null)
                return null;

            var now = DateTime.UtcNow;
            if (ltp.EndDateUtc <= now)
                return "ESAURITO";

            var hoursLeft = (ltp.EndDateUtc - now).TotalHours;
            if (hoursLeft <= 1)
                return "ULTIMA ORA";
            if (hoursLeft <= settings.DynamicBadgeUrgentHours)
                return $"ULTIME {(int)Math.Ceiling(hoursLeft)}H";

            if (ltp.InitialQuantity > 0)
            {
                var pctLeft = 100.0 * ltp.RemainingQuantity / ltp.InitialQuantity;
                if (ltp.RemainingQuantity <= 0)
                    return "SOLD OUT";
                if (pctLeft <= settings.DynamicBadgeLowStockPercent)
                    return "QUASI ESAURITO";
                if (ltp.SoldCount >= 10)
                    return $"{ltp.SoldCount} GIÀ VENDUTI";
            }

            if ((now - ltp.StartDateUtc).TotalHours <= 24)
                return "NUOVO DROP";

            return null;
        }

        public double ComputeProgressPercent(LimitedTimeProduct ltp)
        {
            if (ltp == null) return 0;
            if (ltp.ProgressBarMode == 1 && ltp.InitialQuantity > 0)
            {
                var sold = Math.Min(ltp.SoldCount, ltp.InitialQuantity);
                return Math.Round(100.0 * sold / ltp.InitialQuantity, 1);
            }

            var total = (ltp.EndDateUtc - ltp.StartDateUtc).TotalSeconds;
            if (total <= 0) return 100;
            var elapsed = (DateTime.UtcNow - ltp.StartDateUtc).TotalSeconds;
            return Math.Clamp(Math.Round(100.0 * elapsed / total, 1), 0, 100);
        }

        public async Task RecordSocialProofAsync(int productId, string productName, string eventType, string city = null)
        {
            await _socialRepo.InsertAsync(new SocialProofEvent
            {
                ProductId = productId,
                ProductName = productName ?? "Prodotto",
                EventType = eventType ?? "add_to_cart",
                CityOrRegion = city ?? SimulatedCities[Rnd.Next(SimulatedCities.Length)],
                CreatedOnUtc = DateTime.UtcNow
            });

            // cleanup vecchi
            var cutoff = DateTime.UtcNow.AddDays(-2);
            var old = await _socialRepo.Table.Where(x => x.CreatedOnUtc < cutoff).ToListAsync();
            foreach (var o in old)
                await _socialRepo.DeleteAsync(o);
        }

        public async Task<IList<SocialProofEvent>> GetRecentSocialProofAsync(int take = 10)
        {
            var list = await _socialRepo.Table
                .OrderByDescending(x => x.CreatedOnUtc)
                .Take(take)
                .ToListAsync();
            return list;
        }

        public async Task ApplyOrderSoldAsync(int productId, int quantity)
        {
            if (quantity <= 0) return;
            var items = await _ltpRepo.Table.Where(x => x.ProductId == productId && x.IsActive).ToListAsync();
            foreach (var ltp in items)
            {
                ltp.SoldCount += quantity;
                if (ltp.InitialQuantity > 0)
                    ltp.RemainingQuantity = Math.Max(0, ltp.RemainingQuantity - quantity);
                await _ltpRepo.UpdateAsync(ltp);
            }
        }

        public bool ShouldBlockPurchase(LimitedTimeProduct ltp, LimitedTimeSettings settings)
        {
            if (ltp == null) return false;
            var block = ltp.BlockPurchaseWhenExpired || (settings?.GlobalBlockPurchaseWhenExpired ?? false);
            if (!block) return false;
            if (DateTime.UtcNow > ltp.EndDateUtc) return true;
            if (ltp.InitialQuantity > 0 && ltp.RemainingQuantity <= 0) return true;
            return false;
        }

        public int ResolveCardTemplateId(LimitedTimeSettings settings, string sessionKey)
        {
            if (settings == null) return 0;
            if (!settings.EnableAbTest)
                return settings.SelectedCardTemplateId;

            var ids = (settings.AbTestTemplateIds ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(s => int.TryParse(s, out var i) ? i : -1)
                .Where(i => i >= 0)
                .ToList();
            if (ids.Count == 0)
                ids = Enum.GetValues(typeof(CardTemplateType)).Cast<int>().ToList();

            // hash stabile per sessione
            var hash = 0;
            if (!string.IsNullOrEmpty(sessionKey))
            {
                foreach (var c in sessionKey)
                    hash = (hash * 31) + c;
            }
            else
            {
                hash = Rnd.Next();
            }
            return ids[Math.Abs(hash) % ids.Count];
        }

        public PublicInfoModel EnrichOffer(PublicInfoModel offer, LimitedTimeProduct ltp, LimitedTimeSettings settings)
        {
            if (offer == null || ltp == null) return offer;

            offer.StartDateUtc = ltp.StartDateUtc;
            offer.InitialQuantity = ltp.InitialQuantity;
            offer.RemainingQuantity = ltp.RemainingQuantity;
            offer.SoldCount = ltp.SoldCount;
            // Mostra stock se flag attivo OPPURE se è stata impostata una quantità
            offer.ShowRemainingStock = ltp.ShowRemainingStock || ltp.InitialQuantity > 0;
            offer.ShowSoldCount = ltp.ShowSoldCount || ltp.SoldCount > 0;
            // Progress sempre visibile di default (disattivabile solo se flag prodotto=false E default globale=false)
            var hideProgress = (settings != null && !settings.DefaultShowProgressBar && !ltp.ShowProgressBar);
            offer.ShowProgressBar = !hideProgress;
            offer.ProgressBarMode = ltp.ProgressBarMode != 0
                ? ltp.ProgressBarMode
                : (settings?.DefaultProgressBarMode ?? 0);
            offer.DiscountPercentage = ltp.DiscountPercentage;
            offer.IsExpired = DateTime.UtcNow > ltp.EndDateUtc;
            offer.IsSoldOut = ltp.InitialQuantity > 0 && ltp.RemainingQuantity <= 0;
            offer.BlockPurchase = ShouldBlockPurchase(ltp, settings);
            offer.ProgressPercent = ComputeProgressPercent(ltp);

            var dyn = ComputeDynamicBadge(ltp, settings);
            if (!string.IsNullOrEmpty(dyn))
            {
                offer.DynamicBadgeText = dyn;
                offer.UseDynamicBadge = true;
            }

            return offer;
        }
    }
}
