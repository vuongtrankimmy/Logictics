using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Systems.Navigations.WebHandler;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.Navigations.WebHandler;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Systems.Navigations.WebHandler
{
    public class NavigationWebHandlerRepository : RepositoryBase<NavigationWebHandlerModel>, INavigationWebHandlerRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.NavigationWebHandler.ToString();
        public NavigationWebHandlerRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<NavigationWebHandlerModel>> Page(FilterDefinition<NavigationWebHandlerModel> filter = null, SortDefinition<NavigationWebHandlerModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<NavigationWebHandlerModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<NavigationWebHandlerModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<NavigationWebHandlerModel>> ListByLimit(FilterDefinition<NavigationWebHandlerModel> filter = null, SortDefinition<NavigationWebHandlerModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<NavigationWebHandlerModel> Single(FilterDefinition<NavigationWebHandlerModel> filter = null, SortDefinition<NavigationWebHandlerModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<NavigationWebHandlerModel> SingleById(string _id) => await query.SingleById<NavigationWebHandlerModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public new async Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<NavigationWebHandlerModel> Post(NavigationWebHandlerModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds(string prefix="") => await query.SequenceIds(database, table,prefix);
        public new async Task<IndexManagementModel> SequenceIdsGet(string prefix = "") => await query.SequenceIdsGet(database, table, prefix);
    }
}
