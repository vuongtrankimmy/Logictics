using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Systems.CodesTypes.CodesType
{
    [BsonIgnoreExtraElements]
    public partial class CodesTypeModel:BaseModel
    {
        [JsonProperty("CodeTypeId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? CodeTypeId { get; set; }

        [JsonProperty("CodeTypeName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CodeTypeName { get; set; }

        [JsonProperty("CodeTypeFormat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CodeTypeFormat { get; set; }

        [JsonProperty("CodeTypePrefix", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CodeTypePrefix { get; set; }

        [JsonProperty("CodeTypeNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? CodeTypeNumber { get; set; }

        [JsonProperty("CodeTypeHyphen", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CodeTypeHyphen { get; set; }

        [JsonProperty("CodeTypeGroupId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? CodeTypeGroupId { get; set; }

        [JsonProperty("IsPublish", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? IsPublish { get; set; }
        [JsonProperty("Default", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; }
        [JsonProperty("select", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string select { get; set; }
    }
}
