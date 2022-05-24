using Contracts.AQL;
using Contracts.RepositoryBase;
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

namespace Repository.RepositoryBase
{
    public abstract class RepositoryBase<TEntity>: IRepositoryBase<TEntity>
    {
        readonly IQuery query;
        protected RepositoryBase(IQuery query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<TEntity>> Page<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int page = 0, int pageSize = 0) =>await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<TEntity>> GetInList<TEntity>(string database, string table,string fieldIn, string strInList = null, SortDefinition<TEntity> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<TEntity>> GetInListByType<TEntity>(string database, string table, string fieldIn, IEnumerable<ObjectId> InList = null, SortDefinition<TEntity> sort = null, int limit = 0)=>await query.GetInListByType(database,table,fieldIn,InList,sort,limit);
        public async Task<List<TEntity>> GetInListById<TEntity>(string database, string table, string fieldIn, IEnumerable<int> InList = null, SortDefinition<TEntity> sort = null, int limit = 0) => await query.GetInListById(database, table, fieldIn, InList, sort, limit);
        public async Task<List<TEntity>> GetInListBySingleId<TEntity>(string database, string table, string fieldIn, object fieldEq, SortDefinition<TEntity> sort = null, int limit = 0) => await query.GetInListBySingleId(database, table, fieldIn, fieldEq, sort, limit);
        public async Task<List<TEntity>> ListByLimit<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<TEntity> Single<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<TEntity> SingleById<TEntity>(string database, string table, string _id) => await query.SingleById<TEntity>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table,filter);
        public async Task<bool> Delete<TEntity>(string database, string table, string _id) => await query.Delete<TEntity>(database, table, _id);
        public async Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<TEntity> Post<TEntity>(string database, string table, TEntity doc) => await query.Post(database, table, doc);
        public async Task<IndexManagementModel> SequenceIds(string database, string table, string prefix = "") => await query.SequenceIds(database, table, prefix);
        public async Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "") => await query.SequenceIdsGet(database, table, prefix);
    }
}
