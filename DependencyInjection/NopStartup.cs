using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.LimitedEdition.Services;

namespace Nop.Plugin.Widgets.LimitedEdition.DependencyInjection
{
    public class NopStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILimitedTimeProductService, LimitedTimeProductService>();
            services.AddScoped<ILimitedEditionFeatureService, LimitedEditionFeatureService>();
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 3000;
    }
}
