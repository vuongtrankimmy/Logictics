using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Staffs.StaffGroup;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Staffs.StaffGroup
{
    public interface IStaffGroupRepository : IRepositoryBase<StaffGroupModel>
    {
        Task<TEntityPaging<StaffGroupModel>> Page(FilterDefinition<StaffGroupModel> filter = null, SortDefinition<StaffGroupModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<StaffGroupModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<StaffGroupModel> sort = null, int limit = 0);
        Task<List<StaffGroupModel>> ListByLimit(FilterDefinition<StaffGroupModel> filter = null, SortDefinition<StaffGroupModel> sort = null, int limit = 0);
        Task<StaffGroupModel> Single(FilterDefinition<StaffGroupModel> filter = null, SortDefinition<StaffGroupModel> sort = null, int limit = 0);
        Task<StaffGroupModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<StaffGroupModel> Post(StaffGroupModel doc);
        new Task<IndexManagementModel> SequenceIds(string database, string table);
    }
}
