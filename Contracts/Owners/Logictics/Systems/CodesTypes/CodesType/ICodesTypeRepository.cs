using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.CodesTypes.CodesType;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Systems.CodesTypes.CodesType
{
    public interface ICodesTypeRepository : IRepositoryBase<CodesTypeModel>
    {
        Task<TEntityPaging<CodesTypeModel>> Page(FilterDefinition<CodesTypeModel> filter = null, SortDefinition<CodesTypeModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<CodesTypeModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<CodesTypeModel> sort = null, int limit = 0);
        Task<List<CodesTypeModel>> GetInListById(string fieldIn, IEnumerable<int> strInList = null, SortDefinition<CodesTypeModel> sort = null, int limit = 0);
        Task<List<CodesTypeModel>> ListByLimit(FilterDefinition<CodesTypeModel> filter = null, SortDefinition<CodesTypeModel> sort = null, int limit = 0);
        Task<CodesTypeModel> Single(FilterDefinition<CodesTypeModel> filter = null, SortDefinition<CodesTypeModel> sort = null, int limit = 0);
        Task<CodesTypeModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<CodesTypeModel> Post(CodesTypeModel doc);
        new Task<IndexManagementModel> SequenceIds(string prefix = "");
        new Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "");
    }
}
