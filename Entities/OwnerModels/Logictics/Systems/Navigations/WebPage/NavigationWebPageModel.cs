using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Systems.Navigations.WebPage
{
    [BsonIgnoreExtraElements]
    public partial class NavigationWebPageModel : BaseModel
    {
        [JsonProperty("PageId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? PageId { get; set; }

        [JsonProperty("Name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Handle", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Handle { get; set; }

        [JsonProperty("RelativeUrl", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RelativeUrl { get; set; }
        [JsonProperty("Icon", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("OrderNo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderNo { get; set; }

        [JsonProperty("Visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("Enable", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enable { get; set; }

        [JsonProperty("ParentPageId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? ParentPageId { get; set; }
    }

    public partial class NavigationWebPageTreeModel : BaseModel
    {
        [JsonProperty("PageId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? PageId { get; set; }

        [JsonProperty("Name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("Icon", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("Handle", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Handle { get; set; }

        [JsonProperty("RelativeUrl", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RelativeUrl { get; set; }        

        [JsonProperty("Enable", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enable { get; set; }
        [JsonProperty("ParentPageId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? ParentPageId { get; set; }
        [JsonProperty("HasSub", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string HasSub { get; set; } = "";
        [JsonProperty("Toggle", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Toggle { get; set; } = "";

        public List<NavigationWebPageTreeModel> Sub { get; set; } = new List<NavigationWebPageTreeModel>();
    }    
}
