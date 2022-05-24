using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.AutoMapper
{
    internal static class AutoMapperService
    {
        #region AutoMapper
        /// <summary>
        /// Adds the scoped.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddAutoMapper(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(AccountProfile));
        }
        #endregion
    }
}
