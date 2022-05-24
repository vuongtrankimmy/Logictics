using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Navigations.WebsiteHandler
{
    public class NavigationWebsiteHandlerModel:BaseModel
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Handle", NullValueHandling = NullValueHandling.Ignore)]
        public string Handle { get; set; }

        [JsonProperty("RelativeUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string RelativeUrl { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderNo { get; set; }

        [JsonProperty("Visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("Enable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enable { get; set; }

        [JsonProperty("ParentId", NullValueHandling = NullValueHandling.Ignore)]
        public int? ParentId { get; set; }

        [JsonProperty("CreateDate", NullValueHandling = NullValueHandling.Ignore)]
        public string CreateDate { get; set; }
    }
}
