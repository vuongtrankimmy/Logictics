using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Staffs.StaffPermission;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Staffs.StaffPermission
{
    public interface IStaffPermissionRepository : IRepositoryBase<StaffPermissionModel>
    {
        Task<TEntityPaging<StaffPermissionModel>> Page(FilterDefinition<StaffPermissionModel> filter = null, SortDefinition<StaffPermissionModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<StaffPermissionModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<StaffPermissionModel> sort = null, int limit = 0);
        Task<List<StaffPermissionModel>> ListByLimit(FilterDefinition<StaffPermissionModel> filter = null, SortDefinition<StaffPermissionModel> sort = null, int limit = 0);
        Task<StaffPermissionModel> Single(FilterDefinition<StaffPermissionModel> filter = null, SortDefinition<StaffPermissionModel> sort = null, int limit = 0);
        Task<StaffPermissionModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<StaffPermissionModel> Post(StaffPermissionModel doc);
        new Task<IndexManagementModel> SequenceIds(string database, string table);
    }
}
