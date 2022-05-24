using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.Departments.Department;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Systems.Departments.Department
{
    public interface IDepartmentRepository : IRepositoryBase<DepartmentModel>
    {
        Task<TEntityPaging<DepartmentModel>> Page(FilterDefinition<DepartmentModel> filter = null, SortDefinition<DepartmentModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<DepartmentModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<DepartmentModel> sort = null, int limit = 0);
        Task<List<DepartmentModel>> ListByLimit(FilterDefinition<DepartmentModel> filter = null, SortDefinition<DepartmentModel> sort = null, int limit = 0);
        Task<DepartmentModel> Single(FilterDefinition<DepartmentModel> filter = null, SortDefinition<DepartmentModel> sort = null, int limit = 0);
        Task<DepartmentModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<DepartmentModel> Post(DepartmentModel doc);
        new Task<IndexManagementModel> SequenceIds(string prefix = "");
        new Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "");
    }
}
