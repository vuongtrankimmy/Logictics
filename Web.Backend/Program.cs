using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Web.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                Activity.DefaultIdFormat = ActivityIdFormat.W3C;
                Activity.ForceDefaultIdFormat = true;
                CreateWebHostBuilder(args)
                    .Build().Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of exception");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseWebRoot("notusingwwwroot")
                .UseKestrel(options => options.AddServerHeader = false)
            .ConfigureKestrel(serverOptions =>
            {
            }).UseIISIntegration()
                .ConfigureAppConfiguration(ConfigConfiguration)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.Sources.Remove(
                    configurationBuilder.Sources.First(source =>
                    source.GetType() == typeof(EnvironmentVariablesConfigurationSource))); //remove the default one first
                    configurationBuilder.AddEnvironmentVariables();
                })
                .UseStartup<Startup>().ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();

        private static void ConfigConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder config)
        {
            var pathFile = $"appsettings.env.{ctx.HostingEnvironment.EnvironmentName}.yaml";
            config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.env.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile(pathFile, optional: true, reloadOnChange: true)
            .AddJsonFile("ocelot.json");
            var appSetting = Path.Combine(AppContext.BaseDirectory, pathFile);//dev
            Environment.SetEnvironmentVariable("TWO_APPSETTINGS", appSetting);
        }
    }
}
