using Cores.FilterAttribute;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.Scoped
{
    internal static class ScropedService
    {
        #region Scoped
        /// <summary>
        /// Adds the scoped.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddConfigurationScoped(this IServiceCollection services)
        {
            services.AddScoped<ModelValidationAttribute>();
        }
        #endregion        
    }
}
