using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.ImagesManager;
using Cores.ResponseTime;
using LoggerService.ImagesManager;
using LoggerService.LoggerManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.Logger
{
    internal static class LoggerService
    {
        #region Logger Service
        /// <summary>
        /// Configure Loggers Service
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<IImagesManager, ImagesManager>();
        }
        /// <summary>
        /// Apps the logger service.
        /// </summary>
        /// <param name="app">The app.</param>
        public static void AppLoggerService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseTimeMiddleware>();
        }
        #endregion
    }
}
