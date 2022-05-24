using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Extensions.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cores.Extension.Swagger
{

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IOptions<SwaggerView> SwaggerView;
        private static SwaggerView SwaggerValue { get; set; }
        private static AppDomainSettings DomainSetting { get; set; }
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<SwaggerView> swaggerView, IOptions<AppDomainSettings> appDomainSettings)
        {
            _provider = provider;
            SwaggerView= swaggerView;
            SwaggerValue = SwaggerView.Value;
            DomainSetting = appDomainSettings.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = SwaggerValue.UseSwaggerUi.DocumentTitle,
                Description = SwaggerValue.ChangeLogs,
                //TermsOfService = new Uri(string.Format(_document, _termsOfService)),
                TermsOfService = new Uri(string.Format(DomainSetting.document, SwaggerValue.SwaggerDoc.TermsOfService)),
                Contact = new OpenApiContact
                {
                    Name = SwaggerValue.SwaggerDoc.Contact.Name,
                    Email = SwaggerValue.SwaggerDoc.Contact.Email,
                    Url = new Uri(String.Format(DomainSetting.document, SwaggerValue.SwaggerDoc.Contact.Url))
                }
                ,
                License = new OpenApiLicense
                {
                    Name = SwaggerValue.SwaggerDoc.License.Name,
                    Url = new Uri(string.Format(DomainSetting.document, SwaggerValue.SwaggerDoc.License.Url))
                }
            };


            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }
            return info;
        }       
    }

    public class RemoveVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
    public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
            }
            swaggerDoc.Paths = paths;
        }
    }
}
