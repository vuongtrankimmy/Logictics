using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Systems.CodesTypes.CodesTypeGroup
{
    [BsonIgnoreExtraElements]
    public partial class CodesTypeGroupModel: BaseModel
    {
        [JsonProperty("CodeTypeGroupId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? CodeTypeGroupId { get; set; }

        [JsonProperty("CodeTypeGroupName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CodeTypeGroupName { get; set; }

        [JsonProperty("OrderNo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderNo { get; set; }

        [JsonProperty("Visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("Default", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; }
    }
}
