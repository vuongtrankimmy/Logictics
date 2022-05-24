using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Systems.Positions.Position
{
    public partial class PositionModel: BaseModel
    {
        [JsonProperty("PositionId", NullValueHandling = NullValueHandling.Ignore)]
        public int? PositionId { get; set; }

        [JsonProperty("PositionName", NullValueHandling = NullValueHandling.Ignore)]
        public string PositionName { get; set; }

        [JsonProperty("PositionNameAnsi", NullValueHandling = NullValueHandling.Ignore)]
        public string PositionNameAnsi { get; set; }

        [JsonProperty("PositionNameAcronymn", NullValueHandling = NullValueHandling.Ignore)]
        public string PositionNameAcronymn { get; set; }

        [JsonProperty("Color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("OrderNo", NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderNo { get; set; }

        [JsonProperty("Visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("Default", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; }
        [JsonProperty("Select", NullValueHandling = NullValueHandling.Ignore)]
        public string Select { get; set; }
    }
}
