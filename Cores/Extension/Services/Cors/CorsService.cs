using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.Cors
{
    internal static class CorsService
    {
        #region Configure Cors
        //https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware?view=aspnetcore-6.0
        //UseCors must be called before UseResponseCaching when using CORS middleware.
        //configure CORS is a mechanism that gives rights to the user to access resources from the server on a different domain
        /// <summary>
        /// Configure Cors
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            #region Cors Policy
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
                //.AllowAnyOrigin();
            }));
            #endregion
        }
        #endregion
    }
}
