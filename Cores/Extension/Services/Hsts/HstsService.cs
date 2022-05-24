using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.Hsts
{
    internal static class HstsService
    {
        #region Hsts
        /// <summary>
        ///The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        ///Enforce HTTPS in ASP.NET Core.https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl
        /// </summary>
        /// <param name="services"></param>
        public static void Hsts(this IServiceCollection services)
        {
            #region Hsts
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                //options.ExcludedHosts.Add("example.com");
                //options.ExcludedHosts.Add("www.example.com");
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            #endregion
        }
        #endregion
    }
}
