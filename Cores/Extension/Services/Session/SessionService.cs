using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.Session
{
    internal static class SessionService
    {
        #region Session
        /// <summary>
        /// Configures the session.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
        }
        #endregion
    }
}
