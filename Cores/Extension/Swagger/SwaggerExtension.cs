using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Entities.Extensions.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Cores.Extension.Swagger
{
    /// <summary>
    /// Swagger Extension
    /// </summary>
    public static class SwaggerExtension
    {
        private static string _routeTemplate;
        private static SwaggerView SwaggerView { get; set; }
        /// <summary>
        /// Add Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddSwagger(this IServiceCollection services, IConfiguration config)
        {
            var _document = config.GetSection("DomainSettings:document").Value;
            _routeTemplate = "swagger/{documentName}/swagger.json";
            var configSwagger = config.GetSection("Swagger");
            SwaggerView = configSwagger.Get<SwaggerView>();
            services.Configure<SwaggerView>(configSwagger);

            #region Users ChangeLogs
            var DomainDoc_users = (_document + SwaggerView.SwaggerName + "/").ToLower();
            var changeLogs_users = "<br>***Change Logs*** <br><br>→ **[`v1.0 (20/08/2020)`](" + DomainDoc_users + "changelog/changelog_v1.0.html)**";
            var WhatNew_users = DomainDoc_users + "what-new/what-new.html";
            #endregion            
            #region //API Swagger 4.0.1
            var appName = Assembly.GetEntryAssembly().GetName().Name;
            var version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            services.AddSwaggerGen(swagger =>
            {
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                swagger.IgnoreObsoleteActions();
                swagger.IgnoreObsoleteProperties();                
                swagger.CustomSchemaIds(type => type.FullName.ToLower());
                swagger.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;
                swagger.OperationFilter<SwaggerDefaultValues>();
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = appName+ ": v1 → "+ version,
                    Title = SwaggerView.SwaggerDoc.Title+ " → " + version,
                    Description = changeLogs_users,
                    TermsOfService = new Uri(string.Format(_document, SwaggerView.SwaggerDoc.TermsOfService)),
                    Contact = new OpenApiContact
                    {
                        Name = SwaggerView.SwaggerDoc.Contact.Name,
                        Email = SwaggerView.SwaggerDoc.Contact.Email
                        //Url = new Uri(String.Format(Configuration.GetSection("DomainSettings:document").Value,Configuration.GetSection("Swagger:SwaggerDoc:Contact:Url").Value)),
                    }
                    ,
                    License = new OpenApiLicense
                    {
                        Name = SwaggerView.SwaggerDoc.License.Name,
                        Url = new Uri(_document + SwaggerView.SwaggerDoc.License.Url)
                    }
                });
                //swagger.DescribeAllEnumsAsStrings();
                //swagger.DescribeStringEnumsInCamelCase();
                //swagger.DocumentFilter<LowercaseDocumentFilter>();
                swagger.EnableAnnotations();
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
                swagger.IgnoreObsoleteProperties();

                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                swagger.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>()}
                });

                // add Basic Authentication
                //var basicSecurityScheme = new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "basic",
                //    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
                //};
                //swagger.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
                //swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {basicSecurityScheme, Array.Empty<string>()}
                //});
            });
            #endregion
        }

        /// <summary>
        /// Use Custom Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="apiVersion"></param>
        public static void UseCustomSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersion)
        {
            #region Swagger
            app.UseReDoc(c =>
            {
                c.SpecUrl(String.Format(SwaggerView.UseSwaggerUi.SwaggerEndpoint, SwaggerView.SwaggerName));
                c.EnableUntrustedSpec();
                c.ScrollYOffset(10);
                c.HideHostname();
                c.HideDownloadButton();
                c.ExpandResponses("200,201");
                c.RequiredPropsFirst();
                c.NoAutoAuth();
                c.PathInMiddlePanel();
                c.HideLoading();
                c.NativeScrollbars();
                c.DisableSearch();
                c.OnlyRequiredInSamples();
                c.SortPropsAlphabetically();
            });
            //app.UseSwagger(c => c.SerializeAsV2 = false);
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = false;
                c.RouteTemplate = _routeTemplate;
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                        {new() {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"}};
                });
                c.PreSerializeFilters.Add((document, request) =>
                {
                    var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
                    document.Paths.Clear();
                    foreach (var pathItem in paths)
                    {
                        document.Paths.Add(pathItem.Key, pathItem.Value);
                    }
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.InjectStylesheet(SwaggerView.UseSwaggerUi.InjectStylesheet);
                c.InjectJavascript(SwaggerView.UseSwaggerUi.InjectJavascript);
                foreach (var description in apiVersion.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint(
                    String.Format(SwaggerView.UseSwaggerUi.SwaggerEndpoint, description.GroupName),
                     description.GroupName.ToUpperInvariant());
                }
                c.DocumentTitle = SwaggerView.UseSwaggerUi.DocumentTitle;
                c.RoutePrefix = SwaggerView.UseSwaggerUi.RoutePrefix;
                //c.DocExpansion(DocExpansion.None);
                //c.DefaultModelExpandDepth(2);
                //c.DefaultModelRendering(ModelRendering.Model);
                //c.DefaultModelsExpandDepth(-1);                
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.List);
                c.EnableDeepLinking();
                c.EnableFilter();
                //c.MaxDisplayedTags(5);
                c.ShowExtensions();
                c.EnableValidator();

                //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
                c.RoutePrefix = string.Empty;
            });
            #endregion

            #region Default Page
            DefaultFilesOptions defaultFile = new();
            defaultFile.DefaultFileNames.Clear();
            defaultFile.DefaultFileNames.Add("swagger/index.html");
            app.UseDefaultFiles(defaultFile);
            app.UseStaticFiles();
            #endregion
        }
    }
}
