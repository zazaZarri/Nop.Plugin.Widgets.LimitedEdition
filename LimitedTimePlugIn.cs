using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.LimitedEdition
{
    public class LimitedTimePlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        readonly ILocalizationService localizationService;

        public LimitedTimePlugin(IWebHelper webHelper,
            ISettingService settingService,ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _settingService = settingService;
            this.localizationService = localizationService;
        }

        public bool HideInWidgetList => false;

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
    {
        PublicWidgetZones.HomepageTop,
        PublicWidgetZones.HomepageBeforeProducts,
        PublicWidgetZones.ProductDetailsTop,         
        PublicWidgetZones.OrderSummaryContentBefore 
    });
        }

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(Components.WidgetsLimitedEditionViewComponent);
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/LimitedTime/Configure";
        }

        public override async Task InstallAsync()
        {
            await localizationService.AddOrUpdateLocaleResourceAsync("plugins.widgets.limitededition.settings.backgroundcolor", "BackgroundColor","IT");
            await _settingService.SaveSettingAsync(new LimitedTimeSettings
            {
                BackgroundColor = "#FFCCF0",
                TextColor = "#653212",
                CustomMessage = "Prodotto a tempo limitato! 500 copie rimaste",
                TimerLayout = TimerLayoutType.Horizontal,
                HideProductWhenExpired = false
            });

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<LimitedTimeSettings>();

            await base.UninstallAsync();
        }
    }
}
