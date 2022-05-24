using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Cores.Extension.Services.AntiForgery;
using Cores.Extension.Services.AntiXss;
using Cores.Extension.Services.ApiVersion;
using Cores.Extension.Services.AppSetting;
using Cores.Extension.Services.AutoMapper;
using Cores.Extension.Services.Cors;
using Cores.Extension.Services.CSP;
using Cores.Extension.Services.CustomException;
using Cores.Extension.Services.Gizp;
using Cores.Extension.Services.HealthCheck;
using Cores.Extension.Services.Hsts;
using Cores.Extension.Services.IIS;
using Cores.Extension.Services.JWT;
using Cores.Extension.Services.Languages;
using Cores.Extension.Services.Lazy;
using Cores.Extension.Services.Logger;
using Cores.Extension.Services.MongoDB;
using Cores.Extension.Services.Newtonsoft;
using Cores.Extension.Services.RateLimitRequest;
using Cores.Extension.Services.ResponseCaching;
using Cores.Extension.Services.Scoped;
using Cores.Extension.Services.Session;
using Cores.Extension.Swagger;
using Cores.Extension.Transient;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Cores.ServicesStartup
{
    public static class ServicesStartup
    {
        #region Register All Configuration
        /// <summary>
        /// Registers the configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="Configuration">The configuration.</param>
        public static void RegisterConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            #region config 3.0
            //3.0
            //fix KindValue//Microsoft.AspNetCore.Mvc.NewtonsoftJson
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.ConfigureApiVersion();
            services.ConfigureSession();
            services.Hsts();
            services.NewtonsoftJson();
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureArangoDbContext(Configuration);
            services.AuthenticationJwtBearer(Configuration);
            services.AddSwagger(Configuration);
            services.AddConfigurationScoped();
            services.ConfigureTransientService();
            services.AddAutoMapper();
            services.ConfigureLanguage();
            services.AbpAntiForgeryOptionService();
            services.ConfigureGzip();
            services.ConfigurationResponseCaching();
            services.ConfigurationRateLimitResponse(Configuration);//tam tat: 2022.01.21
            //services.SetupNServiceBus().GetAwaiter().GetResult();
            services.AddConfigureHealthCheck();
            services.AddLazyResolution();//Lazily resolving services to fix circular dependencies in .NET Core
            services.AddOcelot();//https://github.com/ThreeMammals/Ocelot
            #endregion
            #region Connect to appsettings.json
            services.ConfigureAppSetting(Configuration);
            #endregion

        }
        #endregion
        #region Register All Application
        /// <summary>
        /// Registers the application.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="env">The env.</param>
        /// <param name="antiforgery">The antiforgery.</param>
        /// <param name="apiVersion"></param>
        /// <param name="Configuration">The configuration.</param>
        public static void RegisterApplication(this IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery, IApiVersionDescriptionProvider apiVersion, IConfiguration Configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.AppLanguage();
            app.AppCsp();
            app.UseRouting();
            app.ConfigureCustomExceptionMiddleware();
            app.UseCors("CorsPolicy");
            app.AppConfigurationResponseCaching();
            app.ConfigureAuthenticationJwtBearer(Configuration);
            //3.0
            app.AddAntiforgery(antiforgery);
            app.AntiXss();
            app.UseCustomSwagger(apiVersion);
            app.UseResponseCompression();
            //3.0 - Required for routes
            app.ConfigureExceptionHandle();
            app.UseIpRateLimiting();//tam tat: 2022.01.21
            app.AppLoggerService();
            app.AppHealCheck();
            app.UseOcelot().Wait();
        }
        #endregion
    }
}
