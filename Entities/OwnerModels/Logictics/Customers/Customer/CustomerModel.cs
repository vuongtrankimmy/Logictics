using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Customers.Customer
{
    [BsonIgnoreExtraElements]
    public partial class CustomerModel:BaseModel
    {
        
        //[JsonProperty("CustomerId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? CustomerId { get; set; }

        [JsonProperty("CustomerName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerName { get; set; }

        [JsonProperty("CustomerNameAnsi", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerNameAnsi { get; set; }

        [JsonProperty("CustomerNameAcronymn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerNameAcronymn { get; set; }

        [JsonProperty("Color", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("Address1", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Address1 { get; set; }

        [JsonProperty("Address2", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Address2 { get; set; }

        [JsonProperty("WardId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? WardId { get; set; }

        [JsonProperty("DistrictId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? DistrictId { get; set; }

        [JsonProperty("CityId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? CityId { get; set; }

        [JsonProperty("CustomerTypeId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? CustomerTypeId { get; set; }

        [JsonProperty("Staff_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]        
        public string StaffId { get; set; }        

        [JsonProperty("Visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }
    }
}
