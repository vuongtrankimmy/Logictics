using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Staffs.StaffGroup
{
    [BsonIgnoreExtraElements]
    public partial class StaffGroupModel:BaseModel
    {
        [JsonProperty("StaffGroupId", NullValueHandling = NullValueHandling.Ignore)]
        public int? StaffGroupId { get; set; }

        [JsonProperty("GroupName", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupName { get; set; }
        [JsonProperty("GroupDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupDescription { get; set; }

        [JsonProperty("GroupAcronymn", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupAcronymn { get; set; }

        [JsonProperty("GroupColor", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupColor { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderNo { get; set; }

        [JsonProperty("GroupTypeId", NullValueHandling = NullValueHandling.Ignore)]
        public int? GroupTypeId { get; set; }

        [JsonProperty("Visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }
        [JsonProperty("Default", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; } = false;
        [JsonProperty("Select", NullValueHandling = NullValueHandling.Ignore)]
        public string Select { get; set; }
    }
}
