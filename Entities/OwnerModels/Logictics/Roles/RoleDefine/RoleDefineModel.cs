using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Roles.RoleDefine
{
    [BsonIgnoreExtraElements]
    public partial class RoleDefineModel:BaseModel
    {       

        [JsonProperty("RoleDefineId", NullValueHandling = NullValueHandling.Ignore)]
        public int? RoleDefineId { get; set; }

        [JsonProperty("RoleDefineName", NullValueHandling = NullValueHandling.Ignore)]
        public string RoleDefineName { get; set; }

        [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("Visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("Enable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enable { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderNo { get; set; }

        [JsonProperty("OnlyRead", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OnlyRead { get; set; }

        [JsonProperty("RoleDefineList", NullValueHandling = NullValueHandling.Ignore)]
        public List<RoleDefineList> RoleDefineList { get; set; }
    }
    public partial class RoleDefineList
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("RoleList", NullValueHandling = NullValueHandling.Ignore)]
        public List<RoleList> RoleList { get; set; }
    }
    public partial class RoleList
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Check", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Check { get; set; }

        [JsonProperty("OnlyRead", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OnlyRead { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderNo { get; set; }

        [JsonProperty("Visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }
    }
}
