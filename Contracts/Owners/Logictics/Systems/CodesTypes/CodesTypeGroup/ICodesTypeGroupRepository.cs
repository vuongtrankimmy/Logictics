using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.CodesTypes.CodesTypeGroup;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Systems.CodesTypes.CodesTypeGroup
{
    public interface ICodesTypeGroupRepository : IRepositoryBase<CodesTypeGroupModel>
    {
        Task<TEntityPaging<CodesTypeGroupModel>> Page(FilterDefinition<CodesTypeGroupModel> filter = null, SortDefinition<CodesTypeGroupModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<CodesTypeGroupModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<CodesTypeGroupModel> sort = null, int limit = 0);
        Task<List<CodesTypeGroupModel>> GetInListById(string fieldIn, IEnumerable<int> strInList = null, SortDefinition<CodesTypeGroupModel> sort = null, int limit = 0);
        Task<List<CodesTypeGroupModel>> ListByLimit(FilterDefinition<CodesTypeGroupModel> filter = null, SortDefinition<CodesTypeGroupModel> sort = null, int limit = 0);
        Task<CodesTypeGroupModel> Single(FilterDefinition<CodesTypeGroupModel> filter = null, SortDefinition<CodesTypeGroupModel> sort = null, int limit = 0);
        Task<CodesTypeGroupModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<CodesTypeGroupModel> Post(CodesTypeGroupModel doc);
        new Task<IndexManagementModel> SequenceIds(string prefix = "");
        Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "");
    }
}
