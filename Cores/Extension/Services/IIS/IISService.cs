using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.IIS
{
    internal static class IISService
    {
        #region ConfigureIIS
        //IIS integration which will help us with the IIS deployment
        /// <summary>
        /// Configure IIS Intergration
        /// </summary>
        /// <param name="services"></param>
        // ReSharper disable once InconsistentNaming
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(_ =>
            {
            });
        }
        #endregion
    }
}
