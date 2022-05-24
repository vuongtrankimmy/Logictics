using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.OwnerModels.Functions.Staffs.Staff;

namespace BusinessLogicLayer.Owners.Logictics.Staffs.Authentication.Signin
{
    public interface ISigninAuthenticationBAL
    {
        Task<Response> Signin(StaffFuncModel doc);
    }
}
