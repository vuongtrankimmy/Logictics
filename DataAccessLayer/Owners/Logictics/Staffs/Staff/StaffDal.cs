using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Owners.Logictics.Staffs.Staff;
using Contracts.RepositoryWrapper;
using Entities.ExtendedModels.Localize;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.Extensions.NullDySession;
using Entities.Extensions.Response;
using Entities.Extensions.Tokens;
using Entities.OwnerModels.Logictics.Roles.Role;
using Entities.OwnerModels.Logictics.Staffs.Staff;
using Entities.OwnerModels.Logictics.Systems.Departments.Department;
using Entities.OwnerModels.Logictics.Systems.Staffs.StaffStatus;
using Helpers.Helper.Colors;
using Helpers.Helper.Convert;
using Helpers.Helper.Paging;
using Helpers.Helper.UnicodeToAnsi;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLayer.Owners.Logictics.Staffs.Staff
{
    public class StaffDal : IStaffBal
    {
        readonly TokenView tokenView = NullDySession.TokenUser ?? new TokenView();
        readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public StaffDal(IStringLocalizer<Resource> localizer, IRepositoryWrapper repository)
        {
            this.repository = repository;
            this.localizer = localizer;
        }
        #region Paging
        /// <summary>
        /// Phân trang danh sách khách hàng
        /// 2022/04/04
        /// Trần Vương
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">Trang hiện tại hiển thị dữ liệu</param>
        /// <param name="pageSize">Tổng số dữ liệu hiển thị</param>
        /// <returns></returns>
        public async Task<Response> Paging(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            keyword ??= "";
            var bsonRegular = new BsonRegularExpression(keyword, "i");
            var isNumeric = long.TryParse(keyword, out long n);
            var filter =
                  Builders<StaffViewModel>.Filter.Eq("StaffCode", keyword)
                | Builders<StaffViewModel>.Filter.Eq("StaffId", isNumeric)
                | Builders<StaffViewModel>.Filter.Regex("Email", keyword)
                | Builders<StaffViewModel>.Filter.Regex("Phone", keyword)
                | Builders<StaffViewModel>.Filter.Regex("StaffName", keyword)
                | Builders<StaffViewModel>.Filter.Regex("StaffNameAnsi", keyword)
                | Builders<StaffViewModel>.Filter.Regex("StaffNameAcronymn", keyword)
                | Builders<StaffViewModel>.Filter.Regex("Nickname", keyword)
                | Builders<StaffViewModel>.Filter.Regex("IdentityId", keyword);
            var staffList = await repository.StaffRepository.Page(filter, null, pageIndex, pageSize);
            var httpCodeStatus = 404;
            if (staffList != null && staffList.ItemTotal > 0)
            {
                var staffs = staffList.List;
                #region DepartmentId List
                var departmentIdList = staffs.Select(x => x.DepartmentId).Distinct();
                var departmentList = new List<DepartmentModel>();
                if (departmentIdList != null && departmentIdList.Any())
                {
                    var departmentIdJson = ConvertHelper.Serialize(departmentIdList);
                    departmentList = await repository.DepartmentRepository.GetInList("DepartmentId", departmentIdJson);
                }
                #endregion
                #region Creator Id List
                var creatorIdList = staffs.Where(x => x.Creator != null && x.Creator != "").Select(x => ObjectId.Parse(x.Creator)).Distinct();
                var creatorList = new List<StaffModel>();
                if (creatorIdList != null && creatorIdList.Any())
                {
                    //var creatorIdJson = ConvertHelper.Serialize(creatorIdList);
                    creatorList = await repository.StaffRepository.GetInListByType("_id", creatorIdList);
                }
                #endregion
                #region StatusId List
                var staffStatusIdList = staffs.Select(x => x.StatusId).Distinct();
                var staffStatusList = new List<StaffStatusModel>();
                if (staffStatusIdList != null && staffStatusIdList.Any())
                {
                    var staffStatusIdJson = ConvertHelper.Serialize(staffStatusIdList);
                    staffStatusList = await repository.StaffStatusRepository.GetInList("StatusId", staffStatusIdJson);
                }
                #endregion
                #region RoleId List
                var staffRoleIdList = staffs.Where(x => x.RoleId != null && x.RoleId > 0).Select(x => x.RoleId).Distinct();
                var staffRoleList = new List<RoleModel>();
                if (staffRoleIdList != null && staffRoleIdList.Any())
                {
                    var staffRoleIdJson = ConvertHelper.Serialize(staffRoleIdList);
                    staffRoleList = await repository.RoleRepository.GetInList("RoleId", staffRoleIdJson);
                }
                #endregion                
                foreach (var staff in staffs)
                {
                    var departmentId = staff.DepartmentId ?? 0;
                    var statusId = staff.StatusId ?? 0;
                    var roleId = staff.RoleId ?? 0;
                    var creator_id = staff.Creator ?? "";
                    if (departmentId > 0)
                    {
                        if (departmentList != null && departmentList.Any())
                        {
                            var department = departmentList.Where(x => x.DepartmentId == departmentId).FirstOrDefault();
                            if (department != null)
                            {
                                staff.DepartmentName = department.DepartmentName ?? "";
                            }
                        }
                    }
                    if (creator_id != "")
                    {
                        if (creatorList != null && creatorList.Any())
                        {
                            var creator = creatorList.Where(x => x._id.Equals(ObjectId.Parse(creator_id))).FirstOrDefault();
                            if (creator != null)
                            {
                                staff.CreatorName = creator.Nickname ?? creator.StaffName;
                                staff.CreatorAvatar = creator.Avatar;
                                staff.CreatorAcronymn = creator.StaffNameAcronymn;
                            }
                        }
                    }

                    if (statusId > 0)
                    {
                        if (staffStatusList != null && staffStatusList.Any())
                        {
                            var staffStatus = staffStatusList.Where(x => x.StatusId == statusId).FirstOrDefault();
                            if (staffStatus != null)
                            {
                                staff.StatusName = staffStatus.StatusName ?? "";
                                staff.StatusColor = staffStatus.Color ?? "";
                                staff.StatusAcronymn = staffStatus.Acronymn ?? "";
                            }
                        }
                    }

                    if (roleId > 0)
                    {
                        if (staffRoleIdList != null && staffRoleIdList.Any())
                        {
                            var role = staffRoleList.Where(x => x.RoleId == roleId).FirstOrDefault();
                            if (role != null)
                            {
                                staff.OnlyRead = role.OnlyRead;
                            }
                        }
                    }
                }
                httpCodeStatus = 200;
                var page = PagingHelper.Get(keyword, pageIndex, pageSize, decimal.Parse(staffList.ItemTotal.ToString()));
                staffList.Pages = page;
                staffList.Keyword = keyword;
            }
            var response = new Response
            {
                HttpStatusCode = httpCodeStatus,
                Model = staffList
            };
            return response;
        }
        #endregion
        #region Get by _id
        /// <summary>
        /// Lấy thông tin chi tiết khách hàng theo _id
        /// 2022/04/04
        /// Trần Vương
        /// </summary>
        /// <param name="_id">Mã khách hàng theo ObjectId</param>
        /// <returns></returns>
        public async Task<Response> GetBy_id(string _id)
        {
            var httpCodeStatus = 404;
            var staff = new StaffFormModel();
            var departmentId = 0;
            var positionId = 0;
            var staffGroupId = 0;
            var roleId = 0;
            var statusId = 0;
            var permissionId = 0;
            if (_id != null && _id != "")
            {
                staff = await repository.StaffRepository.SingleFormById(_id);
                if (staff?._id != null && staff._id.ToString() != "")
                {
                    departmentId = staff.DepartmentId ?? 0;
                    positionId = staff.PositionId ?? 0;
                    staffGroupId = staff.StaffGroupId ?? 0;
                    roleId = staff.RoleId ?? 0;
                    statusId = staff.StatusId ?? 0;
                    permissionId = staff.PermissionId ?? 0;
                    httpCodeStatus = 200;
                }
            }
            else
            {
                httpCodeStatus = 200;
            }
            var select = "selected";
            var check = "checked";
            var departmentList = await repository.DepartmentRepository.ListByLimit();
            if (departmentId > 0)
            {
                if (departmentList != null && departmentList.Any())
                {
                    var department = departmentList.Where(x => x.DepartmentId.Equals(departmentId)).FirstOrDefault();
                    if (department != null)
                    {
                        staff.DepartmentName = department.DepartmentName ?? "";
                        departmentList.ForEach(x => { x.Default = false; x.Select = ""; });
                        department.Default = true;
                        department.Select = select;

                    }
                }
            }
            staff.DepartmentList = departmentList;

            var staffStatusList = await repository.StaffStatusRepository.ListByLimit();
            if (statusId > 0)
            {
                //var statusFilter = Builders<StaffStatusModel>.Filter.Eq("StatusId", statusId);
                //var status = await repository.StaffStatusRepository.Single(statusFilter);
                if (staffStatusList != null && staffStatusList.Any())
                {
                    var staffStatus = staffStatusList.Where(x => x.StatusId.Equals(statusId)).FirstOrDefault();
                    if (staffStatus != null)
                    {
                        staff.StatusName = staffStatus.StatusName ?? "";
                        staff.StatusColor = staffStatus.Color ?? "";
                        staff.StatusAcronymn = staffStatus.Acronymn ?? "";
                        staffStatusList.ForEach(x => { x.Default = false; x.Check = ""; });
                        staffStatus.Default = true;
                        staffStatus.Check = check;

                    }
                }
            }
            staff.StaffStatusList = staffStatusList;

            var staffGroupList = await repository.StaffGroupRepository.ListByLimit();
            if (staffGroupId > 0)
            {
                //var staffGroupFilter = Builders<StaffGroupModel>.Filter.Eq("StaffGroupId", staffGroupId);
                //var staffGroup = await repository.StaffGroupRepository.Single(staffGroupFilter);
                if (staffGroupList != null && staffGroupList.Any())
                {
                    var staffGroup = staffGroupList.Where(x => x.StaffGroupId.Equals(staffGroupId)).FirstOrDefault();
                    if (staffGroup != null)
                    {
                        staff.StaffGroupName = staffGroup.GroupName ?? "";
                        staffGroupList.ForEach(x => { x.Default = false; x.Select = ""; });
                        staffGroup.Default = true;
                        staffGroup.Select = select;
                    }
                }
            }
            staff.StaffGroupList = staffGroupList;

            var positionList = await repository.PositionRepository.ListByLimit();
            if (positionId > 0)
            {
                //var positionFilter = Builders<PositionModel>.Filter.Eq("PositionId", positionId);
                //var position = await repository.PositionRepository.Single(positionFilter);
                if (positionList != null && positionList.Any())
                {
                    var position = positionList.Where(x => x.PositionId.Equals(positionId)).FirstOrDefault();
                    if (position != null && position.PositionId > 0)
                    {
                        staff.PositionName = position.PositionName ?? "";
                        positionList.ForEach(x => { x.Default = false; x.Select = ""; });
                        position.Default = true;
                        position.Select = select;
                    }
                }
            }
            staff.PositionList = positionList;

            var roleList = await repository.RoleRepository.ListByLimit();
            if (roleId > 0)
            {
                //var roleFilter = Builders<RoleModel>.Filter.Eq("RoleId", roleId);
                //var role = await repository.RoleRepository.Single(roleFilter);
                if (roleList != null && roleList.Any())
                {
                    var role = roleList.Where(x => x.RoleId.Equals(roleId)).FirstOrDefault();
                    if (role != null)
                    {
                        staff.OnlyRead = role.OnlyRead;
                        roleList.ForEach(x => { x.Default = false; x.Select = ""; });
                        role.Default = true;
                        role.Select = select;
                    }
                }
            }
            staff.RoleList = roleList;

            var permissionList = await repository.StaffPermissionRepository.ListByLimit();
            if (permissionId > 0)
            {
                //var roleFilter = Builders<RoleModel>.Filter.Eq("RoleId", roleId);
                //var role = await repository.RoleRepository.Single(roleFilter);
                if (permissionList != null && permissionList.Any())
                {
                    var permission = permissionList.Where(x => x.PermissionId.Equals(permissionId)).FirstOrDefault();
                    if (permission != null)
                    {
                        staff.StaffPermissionName = permission.PermissionName;
                        permissionList.ForEach(x => { x.Default = false; x.Select = ""; });
                        permission.Default = true;
                        permission.Select = select;
                    }
                }
            }
            staff.PermissionList = permissionList;
            var response = new Response
            {
                HttpStatusCode = httpCodeStatus,
                Model = staff
            };
            return response;
        }
        #endregion
        #region Post
        public async Task<Response> Post(StaffModel doc)
        {
            var response = new Response { HttpStatusCode = 200 };
            if (doc == null || doc.StaffName == null)
            {
                response.Message = localizer["doc"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            if (doc.Email == null || doc.Email == "")
            {
                response.Message = localizer["email_blank"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            var createDate = ConvertHelper.Now();

            var filter = Builders<StaffModel>.Filter.Eq("Email", doc.Email)
                | Builders<StaffModel>.Filter.Eq("Phone", doc.Phone)
                | Builders<StaffModel>.Filter.Eq("Nickname", doc.Nickname);
            var staffRepositoryList = await repository.StaffRepository.ListByLimit(filter, null, 10);
            if (staffRepositoryList != null && staffRepositoryList.Any())
            {
                var staffRepository = staffRepositoryList.Where(x => x.Email.Equals(doc.Email)).FirstOrDefault();
                if (staffRepository != null)
                {
                    response.Message = String.Format(localizer["account_exist"].Value, doc.Email, localizer["lbl_email"].Value);
                    response.HttpStatusCode = 204;
                    return response;
                }
                staffRepository = staffRepositoryList.Where(x => x.Phone != "" && x.Phone.Equals(doc.Phone)).FirstOrDefault();
                if (staffRepository != null)
                {
                    response.Message = String.Format(localizer["account_exist"].Value, doc.Phone, localizer["lbl_phone"].Value);
                    response.HttpStatusCode = 204;
                    return response;
                }
            }
            doc.CreateDate = createDate;
            doc.Creator = tokenView.Staff_id;
            doc.User_id = tokenView.User_id;
            doc.Location_id = tokenView.Location_id;

            var staffName = doc.StaffName.ToCapitalize();
            var staffNameAcronymn = staffName.ToAcronymn();
            var staffNameAnsi = UnicodeToAnsiHelper.UnicodeToAnsi(staffName);
            doc.StaffName = staffName;
            doc.StaffNameAcronymn = staffNameAcronymn;
            doc.StaffNameAnsi = staffNameAnsi;
            doc.StaffColor = ColorHelper.Color();
            var staffPost = await repository.StaffRepository.Post(doc);
            if (staffPost != null)
            {
                if (staffPost?._id.ToString() != "")
                {
                    var responseEntity = new ResponseEntityModel
                    {
                        _id = staffPost._id.ToString()
                    };
                    response.Message = String.Format(localizer["response_post"].Value, staffPost._id.ToString());
                    response.Model = responseEntity;
                }
                else
                {
                    response.Message = localizer["account_post"].Value;
                    response.HttpStatusCode = 500;
                    return response;
                }
            }
            else
            {
                response.Message = localizer["account_post"].Value;
                response.HttpStatusCode = 500;
                return response;
            }
            return response;
        }
        #endregion
        #region Put
        public async Task<Response> Put(string _id, StaffModel doc)
        {
            var response = new Response { HttpStatusCode = 200 };
            if (doc == null || doc.StaffName == null)
            {
                response.Message = localizer["doc"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            if (doc.Email == null || doc.Email == "")
            {
                response.Message = localizer["email_blank"].Value;
                return response;
            }
            var createDate = ConvertHelper.Now();
            if (_id != null && _id != "")
            {
                var staffRepository = await repository.StaffRepository.SingleById(_id);
                if (staffRepository != null && staffRepository._id != null && staffRepository._id.ToString() != "")
                {
                    var filter = Builders<StaffModel>.Filter.Eq("_id", ObjectId.Parse(_id));
                    var staffName = doc.StaffName.ToCapitalize();
                    var staffNameAcronymn = staffName.ToAcronymn();
                    var staffNameAnsi = UnicodeToAnsiHelper.UnicodeToAnsi(staffName);
                    var updateDefinition = Builders<StaffModel>.Update
                        .Set("DepartmentId", doc.DepartmentId)
                        .Set("PositionId", doc.PositionId)
                        .Set("PermissionId", doc.PermissionId)
                        .Set("StatusId", doc.StatusId)
                        .Set("RefId", doc.RefId)
                        .Set("IdentityId", doc.IdentityId)
                        .Set("StaffName", staffName)
                        .Set("StaffNameAnsi", staffNameAnsi)
                        .Set("StaffNameAcronymn", staffNameAcronymn)
                        .Set("Avatar", doc.Avatar)
                        .Set("Email", doc.Email)
                        .Set("Phone", doc.Phone)
                        .Set("Nickname", doc.Nickname)
                        .Set("Birthday", doc.Birthday)
                        .Set("Address", doc.Address)
                        .Set("UpdateDate", createDate)
                        ;
                    var staffPost = await repository.StaffRepository.Put(filter, updateDefinition);
                    if (staffPost)
                    {
                        var responseEntity = new ResponseEntityModel
                        {
                            _id = _id
                        };
                        response.Message = String.Format(localizer["response_put"].Value, _id);
                        response.Model = responseEntity;
                    }
                    else
                    {
                        response.Message = localizer["account_post"].Value;
                        response.HttpStatusCode = 500;
                        return response;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Export
        /// <summary>
        /// Export danh sách khách hàng
        /// 2022/05/11
        /// Trần Vương
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">Trang hiện tại hiển thị dữ liệu</param>
        /// <param name="pageSize">Tổng số dữ liệu hiển thị</param>
        /// <returns></returns>
        public async Task<Response> Export(ExportFileModel exportFileModel)
        {
            var exportTypeId = exportFileModel.ExportTypeId ?? 1;
            var staffGroupList = exportFileModel.GroupList;
            var staffGroupJson = "";
            switch (exportTypeId)
            {
                case 2:
                    if (staffGroupList != null && staffGroupList.Any())
                    {
                        var staffGroupIdList = staffGroupList.Select(x => x.GroupId).Distinct();
                        staffGroupJson = ConvertHelper.Serialize(staffGroupIdList);
                    }
                    break;
                default:
                    break;
            }

            var filter =
                  staffGroupJson != "" ? Builders<StaffViewModel>.Filter.In("StaffGroupId", staffGroupJson) : Builders<StaffViewModel>.Filter.Empty;
            var staffList = await repository.StaffRepository.ListViewByLimit(filter);
            var httpCodeStatus = 404;
            if (staffList != null)
            {
                #region DepartmentId List
                var departmentIdList = staffList.Select(x => x.DepartmentId).Distinct();
                var departmentList = new List<DepartmentModel>();
                if (departmentIdList != null && departmentIdList.Any())
                {
                    var departmentIdJson = ConvertHelper.Serialize(departmentIdList);
                    departmentList = await repository.DepartmentRepository.GetInList("DepartmentId", departmentIdJson);
                }
                #endregion
                #region Creator Id List
                var creatorIdList = staffList.Where(x => x.Creator != null && x.Creator != "").Select(x => ObjectId.Parse(x.Creator)).Distinct();
                var creatorList = new List<StaffModel>();
                if (creatorIdList != null && creatorIdList.Any())
                {
                    //var creatorIdJson = ConvertHelper.Serialize(creatorIdList);
                    creatorList = await repository.StaffRepository.GetInListByType("_id", creatorIdList);
                }
                #endregion
                #region StatusId List
                var staffStatusIdList = staffList.Select(x => x.StatusId).Distinct();
                var staffStatusList = new List<StaffStatusModel>();
                if (staffStatusIdList != null && staffStatusIdList.Any())
                {
                    var staffStatusIdJson = ConvertHelper.Serialize(staffStatusIdList);
                    staffStatusList = await repository.StaffStatusRepository.GetInList("StatusId", staffStatusIdJson);
                }
                #endregion
                #region RoleId List
                var staffRoleIdList = staffList.Where(x => x.RoleId != null && x.RoleId > 0).Select(x => x.RoleId).Distinct();
                var staffRoleList = new List<RoleModel>();
                if (staffRoleIdList != null && staffRoleIdList.Any())
                {
                    var staffRoleIdJson = ConvertHelper.Serialize(staffRoleIdList);
                    staffRoleList = await repository.RoleRepository.GetInList("RoleId", staffRoleIdJson);
                }
                #endregion

                var staffs = staffList;
                foreach (var staff in staffs)
                {
                    var departmentId = staff.DepartmentId ?? 0;
                    var statusId = staff.StatusId ?? 0;
                    var roleId = staff.RoleId ?? 0;
                    var creator_id = staff.Creator ?? "";
                    if (departmentId > 0)
                    {
                        if (departmentList != null && departmentList.Any())
                        {
                            var department = departmentList.Where(x => x.DepartmentId == departmentId).FirstOrDefault();
                            if (department != null)
                            {
                                staff.DepartmentName = department.DepartmentName ?? "";
                            }
                        }
                    }
                    if (creator_id != "")
                    {
                        if (creatorList != null && creatorList.Any())
                        {
                            var creator = creatorList.Where(x => x._id.Equals(ObjectId.Parse(creator_id))).FirstOrDefault();
                            if (creator != null)
                            {
                                staff.CreatorName = creator.Nickname ?? creator.StaffName;
                                staff.CreatorAvatar = creator.Avatar;
                                staff.CreatorAcronymn = creator.StaffNameAcronymn;
                            }
                        }
                    }

                    if (statusId > 0)
                    {
                        if (staffStatusList != null && staffStatusList.Any())
                        {
                            var staffStatus = staffStatusList.Where(x => x.StatusId == statusId).FirstOrDefault();
                            if (staffStatus != null)
                            {
                                staff.StatusName = staffStatus.StatusName ?? "";
                                staff.StatusColor = staffStatus.Color ?? "";
                                staff.StatusAcronymn = staffStatus.Acronymn ?? "";
                            }
                        }
                    }

                    if (roleId > 0)
                    {
                        if (staffRoleIdList != null && staffRoleIdList.Any())
                        {
                            var role = staffRoleList.Where(x => x.RoleId == roleId).FirstOrDefault();
                            if (role != null)
                            {
                                staff.OnlyRead = role.OnlyRead;
                            }
                        }
                    }
                }
                httpCodeStatus = 200;
            }
            var response = new Response
            {
                HttpStatusCode = httpCodeStatus,
                Model = staffList
            };
            return response;
        }
        #endregion
    }
}
