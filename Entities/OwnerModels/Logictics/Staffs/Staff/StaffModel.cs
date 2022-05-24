using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using Entities.OwnerModels.Logictics.Roles.Role;
using Entities.OwnerModels.Logictics.Staffs.StaffGroup;
using Entities.OwnerModels.Logictics.Staffs.StaffPermission;
using Entities.OwnerModels.Logictics.Systems.Departments.Department;
using Entities.OwnerModels.Logictics.Systems.Positions.Position;
using Entities.OwnerModels.Logictics.Systems.Staffs.StaffStatus;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Staffs.Staff
{
    [BsonIgnoreExtraElements]
    public partial class StaffModel: StaffInherit
    {        
        [JsonProperty("Password", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }        

        [JsonProperty("PassCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PassCode { get; set; }        
    }

    [BsonIgnoreExtraElements]
    public partial class StaffViewModel : StaffInherit
    {
        [JsonProperty("DepartmentName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentName { get; set; }
        [JsonProperty("PositionName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PositionName { get; set; }
        [JsonProperty("StaffGroupName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaffGroupName { get; set; }
        [JsonProperty("StaffPermissionName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaffPermissionName { get; set; }
        [JsonProperty("LastSigninDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LastSigninDate { get; set; }
        [JsonProperty("OnlyRead", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? OnlyRead { get; set; } = false;
    }
    [BsonIgnoreExtraElements]
    public abstract partial class StaffInherit: StaffBaseInherit
    {       
        public int? DepartmentId { get; set; }

        [JsonProperty("PositionId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? PositionId { get; set; }

        [JsonProperty("RoleId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? RoleId { get; set; }

        [JsonProperty("StatusId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? StatusId { get; set; }
        [JsonProperty("PermissionId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? PermissionId { get; set; }

        [JsonProperty("StaffGroupId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? StaffGroupId { get; set; }

        [JsonProperty("RefId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RefId { get; set; }
        [JsonProperty("IdentityId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string IdentityId { get; set; }       

        [JsonProperty("Visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }
    }
    [BsonIgnoreExtraElements]
    public abstract partial class StaffBaseInherit : BaseModel
    {
        [JsonProperty("StaffId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? StaffId { get; set; }

        [JsonProperty("StaffCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaffCode { get; set; }

        [JsonProperty("StaffName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaffName { get; set; }
        [JsonProperty("StaffColor", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaffColor { get; set; }

        [JsonProperty("StaffNameAnsi", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaffNameAnsi { get; set; }

        [JsonProperty("StaffNameAcronymn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StaffNameAcronymn { get; set; }
        [JsonProperty("Avatar", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Avatar { get; set; }

        [JsonProperty("Email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("Phone", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }        

        [JsonProperty("Nickname", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Nickname { get; set; }
        [JsonProperty("Birthday", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Birthday { get; set; }
        [JsonProperty("Address", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
    }
    [BsonIgnoreExtraElements]
    public partial class StaffFormModel: StaffViewModel
    {
        [JsonProperty("DepartmentList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<DepartmentModel> DepartmentList { get; set; }
        [JsonProperty("PositionList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<PositionModel> PositionList { get; set; }
        [JsonProperty("PermissionList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<StaffPermissionModel> PermissionList { get; set; }
        [JsonProperty("StaffGroupList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<StaffGroupModel> StaffGroupList { get; set; }
        [JsonProperty("StaffStatusList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<StaffStatusModel> StaffStatusList { get; set; }
        [JsonProperty("RoleModel", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<RoleModel> RoleList { get; set; }
    }


    public partial class ExportFileModel
    {
        [JsonProperty("Extension", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Extension { get; set; }
        [JsonProperty("ExportTypeId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? ExportTypeId { get; set; }
        [JsonProperty("GroupList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<ExportGroupList> GroupList { get; set; }        
    }

    public class ExportGroupList
    {
        public string _id { get; set; }
        public int GroupId { get; set; }
    }
}
