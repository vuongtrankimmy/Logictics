using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Staffs.Staff;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Staffs.Staff
{
    public interface IStaffRepository : IRepositoryBase<StaffModel>
    {
        Task<TEntityPaging<StaffViewModel>> Page(FilterDefinition<StaffViewModel> filter = null, SortDefinition<StaffViewModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<StaffModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<StaffModel> sort = null, int limit = 0);
        Task<List<StaffModel>> ListByLimit(FilterDefinition<StaffModel> filter = null, SortDefinition<StaffModel> sort = null, int limit = 0);
        Task<List<StaffViewModel>> ListViewByLimit(FilterDefinition<StaffViewModel> filter = null, SortDefinition<StaffViewModel> sort = null, int limit = 0);
        Task<List<StaffModel>> GetInListByType(string fieldIn, IEnumerable<ObjectId> strInList = null, SortDefinition<StaffModel> sort = null, int limit = 0);
        Task<List<StaffModel>> GetInListById(string fieldIn, IEnumerable<int> strInList = null, SortDefinition<StaffModel> sort = null, int limit = 0);
        Task<StaffModel> Single(FilterDefinition<StaffModel> filter = null, SortDefinition<StaffModel> sort = null, int limit = 0);
        Task<StaffViewModel> SingleById(string _id);
        Task<StaffFormModel> SingleFormById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        Task<bool> Put<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<StaffModel> Post(StaffModel doc);
        new Task<IndexManagementModel> SequenceIds(string database, string table);
    }
}
