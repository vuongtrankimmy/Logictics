using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Systems.Departments.Department
{
    public partial class DepartmentModel:BaseModel
    {
        [JsonProperty("DepartmentId", NullValueHandling = NullValueHandling.Ignore)]
        public int? DepartmentId { get; set; }

        [JsonProperty("DepartmentName", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentName { get; set; }

        [JsonProperty("DepartmentAnsi", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentAnsi { get; set; }

        [JsonProperty("DepartmentAcronymn", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentAcronymn { get; set; }

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
