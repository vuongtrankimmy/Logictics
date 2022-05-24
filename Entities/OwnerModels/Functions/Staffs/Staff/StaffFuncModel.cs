using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Functions.Staffs.Staff
{
    public partial class StaffFuncModel: BaseFunctionModel
    {
        [JsonProperty("UserAccount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string UserAccount { get; set; }

        [JsonProperty("Password", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        [JsonProperty("Remember", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Remember { get; set; }
    }

    public partial class PasswordResult
    {
        public string Password { get; set; }
        public double Calc { get; set; }
        public int Score { get; set; }
    }

    public partial class StaffSignInFuncModel
    {
        [JsonProperty("_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string _id { get; set; }
        [JsonProperty("StaffName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaffName { get; set; }
        [JsonProperty("Email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty("Acronymn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Acronymn { get; set; }
        [JsonProperty("Avatar", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Avatar { get; set; }
        [JsonProperty("PositionName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PositionName { get; set; }
        [JsonProperty("DefaultHref", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultHref { get; set; }
        [JsonProperty("LastLoginDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LastLoginDate { get; set; }
        [JsonProperty("Token", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        [JsonProperty("RefreshToken", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; set; }
        [JsonProperty("ExpireIn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? ExpireIn { get; set; }
        [JsonProperty("Remember", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Remember { get; set; } = false;
        [JsonProperty("IsPermissionFull", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsPermissionFull { get; set; } = false;
    }
}
