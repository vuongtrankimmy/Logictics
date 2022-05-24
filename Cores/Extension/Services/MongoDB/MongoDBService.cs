using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Cores.Extension.Services.MongoDB
{
    internal static class MongoDBService
    {
        /// <summary>
        /// Gets the environment variable.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A string.</returns>
        public static string GetEnvironmentVariable(string name, string defaultValue)
                    => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ?? defaultValue;
        #region ArangoDB | MongoDB
        /// <summary>
        /// Configure ArangoDB Context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureArangoDbContext(this IServiceCollection services, IConfiguration config)
        {
            #region Database connection
            // var environmentName = GetEnvironmentVariable("ARANGO_SERVER", config.GetConnectionString("Arango"));
            // services.AddArango(environmentName);
            //services.Configure<DiaDiemDatabaseSettings>(config.GetSection("ConnectionStrings"));

            //services.Configure<MongoDbSettings>(opt =>
            //{
            //    opt.DiaDiem = config.GetSection("ConnectionStrings:DiaDiem").Value;
            //    //opt.DatabaseName = config.GetSection("ConnectionStrings:DatabaseName").Value;
            //});
            //services.AddSingleton<IMongoContext, MongoDbContext>();
            var environmentName = GetEnvironmentVariable("MONGO_SERVER", config.GetConnectionString("Logictics"));
            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(environmentName);
            });
            #endregion
        }
        #endregion
    }
}
