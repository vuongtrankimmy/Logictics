using BusinessLogicLayer.BusinessWrapper;
using Contracts.AQL;
using Contracts.RepositoryWrapper;
using Contracts.TokenManager;
using DataAccessLayer.BusinessWrapper;
using LoggerService.TokenManager;
using Microsoft.Extensions.DependencyInjection;
using Repository.AQL;
using Repository.RepositoryWrapper;

namespace Cores.Extension.Transient
{
    internal static class TransientService
    {
        #region Transient Service
        /// <summary>
        /// Configure Loggers Service
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureTransientService(this IServiceCollection services)
        {
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddScoped<IBusinessWrapper, BusinessWrapper>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddSingleton<IQuery, Query>();
        }
        #endregion
    }
}
