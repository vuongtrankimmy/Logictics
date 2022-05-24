using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cores.Extension.Services.HealthCheck
{
    internal static class HealthCheckService
    {/// <summary>
     /// Adds the configure health check.
     /// </summary>
     /// <param name="services">The services.</param>
        public static void AddConfigureHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck("Diadiem",
                () =>
                {
                    return HealthCheckResult.Degraded("The check of the service did not work well");
                }
            ).AddCheck("Database",
            () => HealthCheckResult.Healthy("The check of the database worked."));
            //services.AddHealthChecksUI().AddInMemoryStorage();
        }
        public static void AppHealCheck(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/quickhealth", new HealthCheckOptions()
                {
                    Predicate = _ => false
                });
                endpoints.MapHealthChecks("/health/service", new HealthCheckOptions()
                {
                    Predicate = reg => reg.Tags.Contains("service"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                //endpoints.MapHealthChecksUI();
                endpoints.MapControllers();
            });
        }
    }
}
