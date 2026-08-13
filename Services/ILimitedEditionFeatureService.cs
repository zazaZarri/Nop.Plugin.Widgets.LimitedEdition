using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Plugin.Widgets.LimitedEdition.Models;

namespace Nop.Plugin.Widgets.LimitedEdition.Services
{
    public interface ILimitedEditionFeatureService
    {
        string ComputeDynamicBadge(LimitedTimeProduct ltp, LimitedTimeSettings settings);
        double ComputeProgressPercent(LimitedTimeProduct ltp);
        Task RecordSocialProofAsync(int productId, string productName, string eventType, string city = null);
        Task<IList<SocialProofEvent>> GetRecentSocialProofAsync(int take = 10);
        Task ApplyOrderSoldAsync(int productId, int quantity);
        bool ShouldBlockPurchase(LimitedTimeProduct ltp, LimitedTimeSettings settings);
        int ResolveCardTemplateId(LimitedTimeSettings settings, string sessionKey);
        PublicInfoModel EnrichOffer(PublicInfoModel offer, LimitedTimeProduct ltp, LimitedTimeSettings settings);
    }
}
