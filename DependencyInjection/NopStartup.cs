using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
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
        }

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public int Order => 3000;
    }
}
/*
﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace $safeprojectname$.Infrastructure;

public class PluginNopStartup : INopStartup
{
    /// <summary>
    /// Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new ViewLocationExpander());
        });

        //register services and interfaces
        //services.AddScoped<CustomModelFactory, ICustomerModelFactory>();
    }

    /// <summary>
    /// Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 3000;
}
*/