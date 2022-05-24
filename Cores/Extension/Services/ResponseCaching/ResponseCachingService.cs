using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Cores.Extension.Services.ResponseCaching
{
    internal static class ResponseCachingService
    {
        #region Response Caching
        //Phải khai báo sau Cors
        /// <summary>
        /// Configurations the response caching.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigurationResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });
        }
        /// <summary>
        /// Apps the configuration response caching.
        /// </summary>
        /// <param name="app">The app.</param>
        public static void AppConfigurationResponseCaching(this IApplicationBuilder app)
        {
            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                // Microsoft.AspNetCore.Http;
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next.Invoke();
            });
        }
        #endregion
    }
}
