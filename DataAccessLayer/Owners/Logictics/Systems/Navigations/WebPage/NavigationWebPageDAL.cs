using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Owners.Logictics.Systems.Navigations.WebPage;
using Contracts.RepositoryWrapper;
using Entities.ExtendedModels.Localize;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.OwnerModels.Logictics.Systems.Navigations.WebPage;
using Helpers.Helper.RecursiveTree;
using Microsoft.Extensions.Localization;
using MongoDB.Driver;

namespace DataAccessLayer.Owners.Logictics.Systems.Navigations.WebPage
{
    internal class NavigationWebPageDAL: INavigationWebPageBAL
    {
        readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public NavigationWebPageDAL(IStringLocalizer<Resource> localizer, IRepositoryWrapper repository)
        {
            this.repository = repository;
            this.localizer = localizer;
        }

        public async Task<Response> Get()
        {
            var response = new Response { HttpStatusCode = 404 };
            var filter = Builders<NavigationWebPageModel>.Filter.Eq("Visible", true);
            var sort = Builders<NavigationWebPageModel>.Sort.Ascending("OrderNo");
            var navigationWebHandlerList = await repository.NavigationWebPageRepository.ListByLimit(filter, sort);
            if (navigationWebHandlerList != null && navigationWebHandlerList.Any())
            {
                var treeList = await RecursiveTreeHelper.BindTree(navigationWebHandlerList, null);
                response.Model = treeList;
                response.HttpStatusCode = 200;
            }
            return response;
        }

        public async Task<Response> WebsitePageRoute()
        {
            var response = new Response { HttpStatusCode = 404 };
            var filter = Builders<NavigationWebPageModel>.Filter.Eq("Visible", true);
            var sort = Builders<NavigationWebPageModel>.Sort.Ascending("OrderNo");
            var navigationWebHandlerList = await repository.NavigationWebPageRepository.ListByLimit(filter, sort);
            if (navigationWebHandlerList != null && navigationWebHandlerList.Any())
            {
                //var treeList = await RecursiveTreeHelper.BindTree(navigationWebHandlerList, null);
                response.Model = navigationWebHandlerList;
                response.HttpStatusCode = 200;
            }
            return response;
        }
    }
}
