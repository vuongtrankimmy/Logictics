using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Navigations.WebsiteHandler;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Navigations.WebsiteHandler
{
    public interface IWebsiteHandlerRepository
    {
        Task<TEntityPaging<NavigationWebsiteHandlerModel>> Page(FilterDefinition<NavigationWebsiteHandlerModel> filter = null, SortDefinition<NavigationWebsiteHandlerModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<NavigationWebsiteHandlerModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<NavigationWebsiteHandlerModel> sort = null, int limit = 0);
        Task<List<NavigationWebsiteHandlerModel>> ListByLimit(FilterDefinition<NavigationWebsiteHandlerModel> filter = null, SortDefinition<NavigationWebsiteHandlerModel> sort = null, int limit = 0);
        Task<NavigationWebsiteHandlerModel> Single(FilterDefinition<NavigationWebsiteHandlerModel> filter = null, SortDefinition<NavigationWebsiteHandlerModel> sort = null, int limit = 0);
        Task<NavigationWebsiteHandlerModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        Task<bool> Put<TEntity>(Expression<Func<TEntity, bool>> filter, TEntity doc);
        Task<NavigationWebsiteHandlerModel> Post(NavigationWebsiteHandlerModel doc);
        new Task<IndexManagementModel> SequenceIds(string database, string table);
    }
}
