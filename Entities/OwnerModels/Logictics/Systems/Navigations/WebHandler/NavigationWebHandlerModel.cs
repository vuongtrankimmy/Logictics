using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Systems.Navigations.WebHandler
{
    [BsonIgnoreExtraElements]
    public partial class NavigationWebHandlerModel:BaseModel
    {
        [JsonProperty("PageHandlerId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? PageHandlerId { get; set; }

        [JsonProperty("Name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Handle", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Handle { get; set; }

        [JsonProperty("RelativeUrl", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RelativeUrl { get; set; }

        [JsonProperty("OrderNo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderNo { get; set; }

        [JsonProperty("Visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("Enable", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enable { get; set; }

        [JsonProperty("ParentId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? ParentId { get; set; }    
    }
}
