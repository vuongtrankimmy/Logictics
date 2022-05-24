using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Navigations.WebsitePage;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Navigations.WebsitePage;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Navigations.WebsitePage
{
    internal class WebsitePageRepository : RepositoryBase<NavigationWebsitePageModel>, IWebsitePageRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.NavigationWebPage.ToString();
        public WebsitePageRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<NavigationWebsitePageModel>> Page(FilterDefinition<NavigationWebsitePageModel> filter = null, SortDefinition<NavigationWebsitePageModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<NavigationWebsitePageModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<NavigationWebsitePageModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<NavigationWebsitePageModel>> ListByLimit(FilterDefinition<NavigationWebsitePageModel> filter = null, SortDefinition<NavigationWebsitePageModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<NavigationWebsitePageModel> Single(FilterDefinition<NavigationWebsitePageModel> filter = null, SortDefinition<NavigationWebsitePageModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<NavigationWebsitePageModel> SingleById(string _id) => await query.SingleById<NavigationWebsitePageModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public async Task<bool> Put<TEntity>(Expression<Func<TEntity, bool>> filter, TEntity doc) => await query.Put(database, table, filter, doc);
        public async Task<NavigationWebsitePageModel> Post(NavigationWebsitePageModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds(string database, string table) => await query.SequenceIds(database, table);
    }
}
