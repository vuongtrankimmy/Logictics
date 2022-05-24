using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Systems.Units.Unit;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.Units.Unit;
using MongoDB.Bson;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Systems.Units.Unit
{
    public class UnitRepository : RepositoryBase<UnitModel>, IUnitRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.Units.ToString();
        public UnitRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<UnitModel>> Page(FilterDefinition<UnitModel> filter = null, SortDefinition<UnitModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<UnitModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<UnitModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<UnitModel>> GetInListByType(string fieldIn, IEnumerable<ObjectId> inList = null, SortDefinition<UnitModel> sort = null, int limit = 0) => await query.GetInListByType(database, table, fieldIn, inList, sort, limit);
        public async Task<List<UnitModel>> GetInListById(string fieldIn, IEnumerable<int> inList = null, SortDefinition<UnitModel> sort = null, int limit = 0) => await query.GetInListById(database, table, fieldIn, inList, sort, limit);
        public async Task<List<UnitModel>> ListByLimit(FilterDefinition<UnitModel> filter = null, SortDefinition<UnitModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<UnitModel> Single(FilterDefinition<UnitModel> filter = null, SortDefinition<UnitModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<UnitModel> SingleById(string _id) => await query.SingleById<UnitModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public new async Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<UnitModel> Post(UnitModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds(string prefix="") => await query.SequenceIds(database, table,prefix);
    }
}
