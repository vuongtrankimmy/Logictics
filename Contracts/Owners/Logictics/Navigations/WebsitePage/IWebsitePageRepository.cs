using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Navigations.WebsitePage;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Navigations.WebsitePage
{
    public interface IWebsitePageRepository
    {
        Task<TEntityPaging<NavigationWebsitePageModel>> Page(FilterDefinition<NavigationWebsitePageModel> filter = null, SortDefinition<NavigationWebsitePageModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<NavigationWebsitePageModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<NavigationWebsitePageModel> sort = null, int limit = 0);
        Task<List<NavigationWebsitePageModel>> ListByLimit(FilterDefinition<NavigationWebsitePageModel> filter = null, SortDefinition<NavigationWebsitePageModel> sort = null, int limit = 0);
        Task<NavigationWebsitePageModel> Single(FilterDefinition<NavigationWebsitePageModel> filter = null, SortDefinition<NavigationWebsitePageModel> sort = null, int limit = 0);
        Task<NavigationWebsitePageModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        Task<bool> Put<TEntity>(Expression<Func<TEntity, bool>> filter, TEntity doc);
        Task<NavigationWebsitePageModel> Post(NavigationWebsitePageModel doc);
        new Task<IndexManagementModel> SequenceIds(string database, string table);
    }
}
