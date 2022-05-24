using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cores.AntiXss;
using Microsoft.AspNetCore.Builder;

namespace Cores.Extension.Services.AntiXss
{
    internal static class AntiXssService
    {
        #region Anti XSS
        /// <summary>
        /// Antis the xss.
        /// </summary>
        /// <param name="app">The app.</param>
        public static void AntiXss(this IApplicationBuilder app)
        {
            app.UseMiddleware<AntiXssMiddleware>();
        }
        #endregion
    }
}
