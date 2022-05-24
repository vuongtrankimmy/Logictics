using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Staffs.StaffPermission
{
    public class StaffPermissionModel:BaseModel
    {
        [JsonProperty("PermissionId", NullValueHandling = NullValueHandling.Ignore)]
        public int? PermissionId { get; set; }

        [JsonProperty("PermissionName", NullValueHandling = NullValueHandling.Ignore)]
        public string PermissionName { get; set; }

        [JsonProperty("IsPermissionFull", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsPermissionFull { get; set; }

        [JsonProperty("PermissionList", NullValueHandling = NullValueHandling.Ignore)]
        public string PermissionList { get; set; }
        [JsonProperty("Default", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; }
        [JsonProperty("Select", NullValueHandling = NullValueHandling.Ignore)]
        public string Select { get; set; }
    }
}
