using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Roles.Role;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Roles.Role;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Roles.Role
{
    public class RoleRepository : RepositoryBase<RoleModel>, IRoleRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.Roles.ToString();
        public RoleRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<RoleModel>> Page(FilterDefinition<RoleModel> filter = null, SortDefinition<RoleModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<RoleModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<RoleModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<RoleModel>> ListByLimit(FilterDefinition<RoleModel> filter = null, SortDefinition<RoleModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<RoleModel> Single(FilterDefinition<RoleModel> filter = null, SortDefinition<RoleModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<RoleModel> SingleById(string _id) => await query.SingleById<RoleModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public new async Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<RoleModel> Post(RoleModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds(string database, string table) => await query.SequenceIds(database, table);
    }
}
