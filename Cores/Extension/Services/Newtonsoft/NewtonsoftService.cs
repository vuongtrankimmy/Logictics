using Cores.BadRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Cores.Extension.Services.Newtonsoft
{
    internal static class NewtonsoftService
    {
        #region Newtonsoft Json
        /// <summary>
        /// Newtonsofts the json.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void NewtonsoftJson(this IServiceCollection services)
        {
            #region Cors Policy
            _ = services.AddControllers().AddJsonOptions(options =>
              {
                //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;//Bỏ qua giá trị null trả về
                //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;

              }); 
            _ = services.AddMvc(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }
            ).SetCompatibilityVersion(version: CompatibilityVersion.Latest).AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver =
                    new DefaultContractResolver();//Fixed Swagger hiển thị kiểu CamelCase (convert CamelCase-->PascalCase)                
            }).ConfigureApiBehaviorOptions(options =>
            {                
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problems = new CustomBadRequest(context);

                    return new BadRequestObjectResult(problems);
                };
            });
            #endregion
        }
        #endregion
    }
}
