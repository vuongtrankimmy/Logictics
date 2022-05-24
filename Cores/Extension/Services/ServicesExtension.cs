using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using BusinessLogicLayer.BusinessWrapper;
using Contracts;
using Contracts.AQL;
using Contracts.ImagesManager;
using Contracts.RepositoryWrapper;
using Contracts.TokenManager;
using Cores.AntiXss;
using Cores.BadRequest;
using Cores.Exception.CustomExceptionMiddleware;
using Cores.Extension.Swagger;
using Cores.FilterAttribute;
using Cores.Jwt;
using Cores.ResponseTime;
using DataAccessLayer.BusinessWrapper;
using HealthChecks.UI.Client;
using Jaeger;
using Jaeger.Samplers;
using LoggerService.ImagesManager;
using LoggerService.LoggerManager;
using LoggerService.TokenManager;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OpenTracing;
using OpenTracing.Util;
using RabbitMQ.Client;
using Repository.AQL;
using Repository.RepositoryWrapper;

namespace Cores.Extension.Services
{
    /// <summary>
    /// Service Extension
    /// </summary>
    public static class ServicesExtension
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
            services.AddTransient<IQuery, Query>();
        }
        #endregion
    }
}
