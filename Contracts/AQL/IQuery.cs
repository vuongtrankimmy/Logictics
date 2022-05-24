using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.AQL
{
    public interface IQuery
    {
        Task<TEntityPaging<TEntity>> Page<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int pageIndex = 0, int pageSize = 0);
        Task<List<TEntity>> GetInList<TEntity>(string database, string table,string fieldIn, string strInList = null, SortDefinition<TEntity> sort = null, int limit = 0);
        Task<List<TEntity>> GetInListByType<TEntity>(string database, string table, string fieldIn, IEnumerable<ObjectId> InList = null, SortDefinition<TEntity> sort = null, int limit = 0);
        Task<List<TEntity>> GetInListById<TEntity>(string database, string table, string fieldIn, IEnumerable<int> InList = null, SortDefinition<TEntity> sort = null, int limit = 0);
        Task<List<TEntity>> GetInListBySingleId<TEntity>(string database, string table, string fieldIn, object fieldEq, SortDefinition<TEntity> sort = null, int limit = 0);
        Task<List<TEntity>> ListByLimit<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int limit = 0);
        Task<TEntity> Single<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int limit = 0);
        Task<TEntity> SingleById<TEntity>(string database, string table, string _id);
        Task<long> TotalItem<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string database, string table, string _id);
        Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<TEntity> Post<TEntity>(string database, string table, TEntity doc);
        Task<IndexManagementModel> SequenceIds(string database, string table, string prefix = "");
        Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "");
    }
}
