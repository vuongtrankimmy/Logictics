using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Cores.Extension.Services.Environment
{
    internal static class EnvironmentService
    {
        #region Environment
        /// <summary>
        /// Adds the environment.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="env">The env.</param>
        public static void AddEnvironment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Development environment code
            }
            else if (env.IsStaging())
            {
                // Staging environment code
            }
            else if (env.IsProduction())
            {
                // Code for all other environments
            }
        }
        #endregion
    }
}
