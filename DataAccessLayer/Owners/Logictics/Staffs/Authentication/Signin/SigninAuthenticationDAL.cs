using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer.Owners.Logictics.Staffs.Authentication.Signin;
using Contracts.RepositoryWrapper;
using Contracts.TokenManager;
using Entities.ExtendedModels.Localize;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.OwnerModels.Functions.Staffs.Staff;
using Entities.OwnerModels.Logictics.Staffs.Staff;
using Entities.OwnerModels.Logictics.Staffs.StaffPermission;
using Entities.OwnerModels.Logictics.Systems.Positions.Position;
using Helpers.Helper.Convert;
using Helpers.Helper.Globals;
using Microsoft.Extensions.Localization;
using MongoDB.Driver;

namespace DataAccessLayer.Owners.Logictics.Staffs.Authentication.Signin
{
    public class SigninAuthenticationDAL: ISigninAuthenticationBAL
    {
        readonly ITokenManager tokenManager;
        readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public SigninAuthenticationDAL(IStringLocalizer<Resource> localizer, IRepositoryWrapper repository, ITokenManager tokenManager)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.tokenManager = tokenManager;
        }
        public async Task<Response> Signin(StaffFuncModel doc)
        {
            var response = new Response { HttpStatusCode = 200 };
            var culture = Thread.CurrentThread.CurrentCulture.TextInfo.CultureName.ToString();
            if (doc == null || (doc.UserAccount == null || doc.Password == null))
            {
                response.Message = localizer["doc"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            var language = doc.Language;
            var userAccount = doc.UserAccount ?? "";
            if (userAccount == "")
            {
                response.Message = localizer["username"].Value;
                return response;
            }
            var password = doc.Password ?? "";
            if (password == "")
            {
                response.Message = localizer["password"].Value;
                return response;
            }
            if (userAccount != "" && password != "")
            {
                var filter = Builders<StaffModel>.Filter.Eq("Email", userAccount) | Builders<StaffModel>.Filter.Eq("Phone", userAccount) | Builders<StaffModel>.Filter.Eq("StaffCode", userAccount) | Builders<StaffModel>.Filter.Eq("Nickname", userAccount);
                var staffRepositoryList = await repository.StaffRepository.ListByLimit(filter, null, 10);
                if (staffRepositoryList != null && staffRepositoryList.Any())
                {
                    var enPassword = ConvertHelper.PasswordEncrypt(password);
                    var currentPassword = enPassword.Password ?? "";
                    var accountSignin = staffRepositoryList.Where(x => x.Password.Equals(currentPassword)).FirstOrDefault();

                    if (accountSignin != null)
                    {
                        //Chỗ này cần viết thêm logic và token và permission.
                        var user_id = accountSignin._id.ToString() ?? "";
                        var acronymn = accountSignin.StaffNameAcronymn ?? "";
                        var staffName = accountSignin.Nickname ?? accountSignin.StaffName;
                        var email = accountSignin.Email ?? accountSignin.Phone;
                        var avatar = accountSignin.Avatar ?? "";
                        var permissionId = accountSignin.PermissionId ?? 0;
                        var positionId = accountSignin.PositionId ?? 0;
                        var defaultHref = "error/401";
                        var isPermissionFull = false;
                        if (permissionId > 0)
                        {
                            var permissionFilter = Builders<StaffPermissionModel>.Filter.And(Builders<StaffPermissionModel>.Filter.Eq("PermissionId", permissionId));
                            var permission = await repository.StaffPermissionRepository.Single(permissionFilter);
                            if (permission != null)
                            {
                                isPermissionFull = permission.IsPermissionFull ?? isPermissionFull;
                                switch (isPermissionFull)
                                {
                                    case true:
                                        defaultHref = "san-pham";
                                        break;
                                    default:
                                        var permissionList = permission.PermissionList ?? "";
                                        if (permissionList != "")
                                        {
                                            defaultHref = "staff";
                                        }
                                        break;
                                }
                            }
                        }
                        var positionName = "";
                        if (positionId > 0)
                        {
                            var positionFilter = Builders<PositionModel>.Filter.And(Builders<PositionModel>.Filter.Eq("PositionId", positionId));
                            var position = await repository.PositionRepository.Single(positionFilter);
                            if (position != null)
                            {
                                positionName = position.PositionName ?? "";
                            }
                        }
                        if (user_id != "")
                        {
                            //var token = new Tokens();
                            var claim = new Claim[] {
                                new Claim("User_id",user_id),
                                new Claim("Location_id",""),
                                new Claim("Device_id",""),
                                new Claim("Staff_id",user_id),
                                new Claim("Language",language)
                            };
                            var generateToken = tokenManager.GenerateJwtToken(claim);
                            var refreshToken = tokenManager.GenerateJwtRefresh();
                            if (generateToken != null)
                            {
                                var staffSigninFuncModel = new StaffSignInFuncModel
                                {
                                    _id = user_id,
                                    StaffName = staffName,
                                    Email = email,
                                    Acronymn = acronymn,
                                    Avatar = avatar,
                                    PositionName = positionName,
                                    DefaultHref = defaultHref,
                                    LastLoginDate = "",
                                    Token = generateToken,
                                    RefreshToken = refreshToken,
                                    ExpireIn = GlobalsHelper.TokenExpireIn,
                                    Remember = doc.Remember ?? false,
                                    IsPermissionFull = isPermissionFull
                                };
                                response.Model = staffSigninFuncModel;
                                response.HttpStatusCode = 200;
                            }
                        }
                        else
                        {
                            response.Message = string.Format(localizer["500"].Value, userAccount);
                            response.HttpStatusCode = 500;
                        }
                    }
                    else
                    {
                        //Mật khẩu không đúng
                        response.Message = string.Format(localizer["validate_signin"].Value, userAccount);
                        response.HttpStatusCode = 401;
                    }
                }
                else
                {
                    //Mật khẩu không đúng
                    response.Message = string.Format(localizer["validate_signin"].Value, userAccount);
                    response.HttpStatusCode = 404;
                }
            }
            return response;
        }
    }
}
