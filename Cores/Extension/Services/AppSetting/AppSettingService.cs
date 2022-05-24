using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.AppSetting
{
    internal static class AppSettingService
    {
        #region App Setting
        /// <summary>
        /// Configures the app setting.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="config">The config.</param>
        public static void ConfigureAppSetting(this IServiceCollection services, IConfiguration config)
        {
            #region Database connection
            var appIdentitySettings = config.GetSection("AppIdentitySettings");
            var appDBSettings = config.GetSection("AppDBSettings");
            var tokenManagement = config.GetSection("TokenManagement");
            var appsAuthenticate = config.GetSection("AppsAuthenticate");
            var domainSettings = config.GetSection("DomainSettings");

            services.Configure<AppIdentitySettings>(appIdentitySettings);
            services.Configure<AppDBSettings>(appDBSettings);
            services.Configure<TokenManagement>(tokenManagement);
            services.Configure<AppsAuthenticate>(appsAuthenticate);
            services.Configure<AppDomainSettings>(domainSettings);
            #endregion
        }
        #endregion
    }
}
