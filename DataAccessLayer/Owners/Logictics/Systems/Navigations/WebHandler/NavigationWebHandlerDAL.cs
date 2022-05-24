using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Owners.Logictics.Systems.Navigations.WebHandler;
using Contracts.RepositoryWrapper;
using Entities.ExtendedModels.Localize;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.OwnerModels.Logictics.Systems.Navigations.WebHandler;
using Microsoft.Extensions.Localization;
using MongoDB.Driver;

namespace DataAccessLayer.Owners.Logictics.Systems.Navigations.WebHandler
{
    internal class NavigationWebHandlerDAL : INavigationWebHandlerBAL
    {
        readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public NavigationWebHandlerDAL(IStringLocalizer<Resource> localizer, IRepositoryWrapper repository)
        {
            this.repository = repository;
            this.localizer = localizer;
        }

        public async Task<Response> Get()
        {
            var response = new Response { HttpStatusCode = 404 };
            var filter = Builders<NavigationWebHandlerModel>.Filter.Eq("Visible", true);
            var sort = Builders<NavigationWebHandlerModel>.Sort.Descending("OrderNo");
            var navigationWebHandlerList = await repository.NavigationWebHandlerRepository.ListByLimit(filter, sort);
            if (navigationWebHandlerList != null && navigationWebHandlerList.Any())
            {
                response.Model = navigationWebHandlerList;
                response.HttpStatusCode = 200;
            }
            return response;
        }
    }
}
