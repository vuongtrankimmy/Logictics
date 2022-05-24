using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Roles.Role
{
    [BsonIgnoreExtraElements]
    public partial class RoleModel: BaseModel
    {
        [JsonProperty("RoleId", NullValueHandling = NullValueHandling.Ignore)]
        public int? RoleId { get; set; }

        [JsonProperty("RoleName", NullValueHandling = NullValueHandling.Ignore)]
        public string RoleName { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderNo { get; set; }

        [JsonProperty("Visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("OnlyRead", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OnlyRead { get; set; }
        [JsonProperty("Default", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; } = false;
        [JsonProperty("Select", NullValueHandling = NullValueHandling.Ignore)]
        public string Select { get; set; }
    }
}
