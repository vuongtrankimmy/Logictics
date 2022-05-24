using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Systems.Staffs.StaffStatus
{
    [BsonIgnoreExtraElements]
    public partial class StaffStatusModel
    {
        [JsonProperty("StatusId", NullValueHandling = NullValueHandling.Ignore)]
        public int? StatusId { get; set; }

        [JsonProperty("StatusName", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusName { get; set; }

        [JsonProperty("Color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }
        [JsonProperty("Icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("Acronymn", NullValueHandling = NullValueHandling.Ignore)]
        public string Acronymn { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderNo { get; set; }

        [JsonProperty("Visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("Default", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; }
        [JsonProperty("Check", NullValueHandling = NullValueHandling.Ignore)]
        public string Check { get; set; }
    }
}
