using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Owners.Logictics.Staffs.StaffGroup;
using Contracts.RepositoryWrapper;
using Entities.ExtendedModels.Localize;
using Entities.ExtendedModels.MethodErrorHandling;
using Microsoft.Extensions.Localization;

namespace DataAccessLayer.Owners.Logictics.Staffs.StaffGroup
{
    public class StaffGroupDal : IStaffGroupBal
    {
        readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public StaffGroupDal(IStringLocalizer<Resource> localizer, IRepositoryWrapper repository)
        {
            this.repository = repository;
            this.localizer = localizer;
        }

        #region Get by _id
        /// <summary>
        /// Lấy thông tin chi tiết khách hàng theo _id
        /// 2022/04/04
        /// Trần Vương
        /// </summary>
        /// <param name="_id">Mã khách hàng theo ObjectId</param>
        /// <returns></returns>
        public async Task<Response> GetList()
        {
            var httpCodeStatus = 404;
            var staffGroupList = await repository.StaffGroupRepository.ListByLimit();            
            if(staffGroupList != null&& staffGroupList.Any())
            {
                httpCodeStatus = 200;
            }
            var response = new Response
            {
                HttpStatusCode = httpCodeStatus,
                Model = staffGroupList
            };
            return response;
        }
        #endregion
    }
}
