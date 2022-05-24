using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.Staffs.StaffStatus;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Systems.Staffs.StaffStatus
{
    public interface IStaffStatusRepository : IRepositoryBase<StaffStatusModel>
    {
        Task<TEntityPaging<StaffStatusModel>> Page(FilterDefinition<StaffStatusModel> filter = null, SortDefinition<StaffStatusModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<StaffStatusModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<StaffStatusModel> sort = null, int limit = 0);
        Task<List<StaffStatusModel>> ListByLimit(FilterDefinition<StaffStatusModel> filter = null, SortDefinition<StaffStatusModel> sort = null, int limit = 0);
        Task<StaffStatusModel> Single(FilterDefinition<StaffStatusModel> filter = null, SortDefinition<StaffStatusModel> sort = null, int limit = 0);
        Task<StaffStatusModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<StaffStatusModel> Post(StaffStatusModel doc);
        new Task<IndexManagementModel> SequenceIds(string prefix = "");
        Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "");
    }
}
