using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.Zipkin
{
    internal static class ZipkinService
    {
        #region Zipkin
        /// <summary>
        /// Adds the zipkin.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddZipkin(this IServiceCollection services)
        {
            //services.AddOpenTelemetryTracing(builder => builder
            //     .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(Program.EndpointName))
            //     .AddAspNetCoreInstrumentation(opt => opt.Enrich = (activity, key, value) =>
            //     {
            //         Console.WriteLine($"Got an activity named {key}");
            //     })
            //     .AddSqlClientInstrumentation(opt => opt.SetDbStatementForText = true)
            //     .AddNServiceBusInstrumentation()
            //     .AddZipkinExporter(o =>
            //     {
            //         o.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
            //     })
            //     .AddJaegerExporter(c =>
            //     {
            //         c.AgentHost = "localhost";
            //         c.AgentPort = 6831;
            //     })
            // );
        }
        #endregion
    }
}
