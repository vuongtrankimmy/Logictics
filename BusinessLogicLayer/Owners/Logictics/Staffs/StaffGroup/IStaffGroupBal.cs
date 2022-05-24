using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.MethodErrorHandling;

namespace BusinessLogicLayer.Owners.Logictics.Staffs.StaffGroup
{
    public interface IStaffGroupBal
    {
        Task<Response> GetList();
    }
}
