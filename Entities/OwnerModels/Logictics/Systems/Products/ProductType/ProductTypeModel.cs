using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Systems.Products.ProductType
{
    [BsonIgnoreExtraElements]
    public partial class ProductTypeModel:BaseModel
    {
        [JsonProperty("ProductTypeId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? ProductTypeId { get; set; }

        [JsonProperty("ProductTypeName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductTypeName { get; set; }

        [JsonProperty("OrderNo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderNo { get; set; }

        [JsonProperty("Visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }
        [JsonProperty("Default", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; }
        [JsonProperty("check", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string check { get; set; }
    }
}
