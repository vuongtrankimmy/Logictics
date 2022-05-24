using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.OwnerModels.Functions.Staffs.Staff;
using Entities.OwnerModels.Logictics.Staffs.Staff;

namespace BusinessLogicLayer.Owners.Logictics.Staffs.Staff
{
    public interface IStaffBal
    {
        Task<Response> Paging(string keyword = "", int pageIndex = 1, int pageSize = 10);
        Task<Response> GetBy_id(string _id);
        Task<Response> Post(StaffModel doc);
        Task<Response> Put(string _id, StaffModel doc);
        Task<Response> Export(ExportFileModel exportFileModel);
    }
}
