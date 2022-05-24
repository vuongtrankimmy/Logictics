using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Cores.Extension.Services.ApiVersion
{
    internal static class ApiVersionService
    {
        #region Api Version
        /// <summary>
        ///Today in this article, We shall see high-level options to enable REST API Versioning in ASP.NET Core to evolve API for new requirements along with protecting existing API’s capability from any breaking changes.
        ///https://www.thecodebuzz.com/api-versioning-in-asp-net-core-with-examples/
        ///Install-Package Microsoft.AspNetCore.Mvc.Versioning -Version 5.0.0
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApiVersion(this IServiceCollection services)
        {

            //Microsoft.AspNetCore.Mvc.Versioning - A service API versioning library for Microsoft ASP.NET Core
            //Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer - Discovering metadata such as the list of API - versioned controllers and actions, and their URLs and allowed HTTP methods
            //Swashbuckle.AspNetCore - Swagger tools for documenting APIs built on ASP.NET Core

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                //options.ApiVersionReader = new QueryStringApiVersionReader();
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                //options.ApiVersionReader = new HeaderApiVersionReader("v");
                //options.ApiVersionReader = new MediaTypeApiVersionReader("v");
                //options.ApiVersionReader =
                //                ApiVersionReader.Combine(
                //                   new HeaderApiVersionReader("v"),
                //                   new QueryStringApiVersionReader("v"));
            });

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service  
                // note: the specified format code will format the version as "'v'major[.minor][-status]"  
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat  
                // can also be used to control the format of the API version in route templates  
                options.SubstituteApiVersionInUrl = true;
            });
        }
        #endregion
    }
}
