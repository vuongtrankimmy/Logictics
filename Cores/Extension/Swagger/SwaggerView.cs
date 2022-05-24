using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Entities.Extensions.Swagger
{
    public partial class SwaggerView
    {
        [JsonProperty("SwaggerName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string SwaggerName { get; set; }

        [JsonProperty("SwaggerDoc", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public SwaggerDoc SwaggerDoc { get; set; }

        [JsonProperty("UseSwaggerUI", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public UseSwaggerUi UseSwaggerUi { get; set; }
        [JsonProperty("ChangeLogs", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeLogs { get; set; }
        [JsonProperty("WhatNew", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string WhatNew { get; set; }
    }

    public partial class SwaggerDoc
    {
        [JsonProperty("Version", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; } = Assembly.GetEntryAssembly().GetName().Name+ " → "+ Assembly.GetEntryAssembly().GetName().Version.ToString();

        [JsonProperty("Title", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("Description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("TermsOfService", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string TermsOfService { get; set; }

        [JsonProperty("Contact", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Contact Contact { get; set; }

        [JsonProperty("License", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public License License { get; set; }
    }

    public partial class Contact
    {
        [JsonProperty("Name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("Url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }

    public partial class License
    {
        [JsonProperty("Name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }

    public partial class UseSwaggerUi
    {
        [JsonProperty("SwaggerEndpoint", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string SwaggerEndpoint { get; set; }

        [JsonProperty("Name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("DocumentTitle", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentTitle { get; set; }

        [JsonProperty("RoutePrefix", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoutePrefix { get; set; }

        [JsonProperty("InjectStylesheet", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string InjectStylesheet { get; set; }

        [JsonProperty("InjectJavascript", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string InjectJavascript { get; set; }

        [JsonProperty("InjectJavascript1", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string InjectJavascript1 { get; set; }
    }
}
