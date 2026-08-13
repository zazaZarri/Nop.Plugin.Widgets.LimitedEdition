using System.Collections.Generic;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    public class HomepageOffersModel
    {
        public HomepageOffersModel()
        {
            Offers = new List<PublicInfoModel>();
        }

        public bool IsProductPage { get; set; }
        public IList<PublicInfoModel> Offers { get; set; }
        public StyleSettingsModel Style { get; set; }
        public int CardTemplateId { get; set; }
        public int PopupTemplateId { get; set; }

        // Feature flags esposti alla view
        public bool EnableSocialProof { get; set; }
        public int SocialProofIntervalSeconds { get; set; }
        public bool EnableLastHourSound { get; set; }
        public bool EnableServerCountdown { get; set; }
        public bool CompactLayout { get; set; }
        public string WidgetZone { get; set; }
        public string ServerCountdownUrl { get; set; }
        public string SocialProofFeedUrl { get; set; }
        public bool UseProductImageAsBackground { get; set; }
    }
}
