﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.Navigations.WebHandler;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Systems.Navigations.WebHandler
{
    public interface INavigationWebHandlerRepository : IRepositoryBase<NavigationWebHandlerModel>
    {
        Task<TEntityPaging<NavigationWebHandlerModel>> Page(FilterDefinition<NavigationWebHandlerModel> filter = null, SortDefinition<NavigationWebHandlerModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<NavigationWebHandlerModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<NavigationWebHandlerModel> sort = null, int limit = 0);
        Task<List<NavigationWebHandlerModel>> ListByLimit(FilterDefinition<NavigationWebHandlerModel> filter = null, SortDefinition<NavigationWebHandlerModel> sort = null, int limit = 0);
        Task<NavigationWebHandlerModel> Single(FilterDefinition<NavigationWebHandlerModel> filter = null, SortDefinition<NavigationWebHandlerModel> sort = null, int limit = 0);
        Task<NavigationWebHandlerModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<NavigationWebHandlerModel> Post(NavigationWebHandlerModel doc);
        new Task<IndexManagementModel> SequenceIds(string prefix="");
        new Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "");
    }
}
