using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Owners.Logictics.Customers.Customer;
using Contracts.RepositoryWrapper;
using Entities.ExtendedModels.Localize;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.Extensions.Response;
using Entities.OwnerModels.Logictics.Customers.Customer;
using Microsoft.Extensions.Localization;
using MongoDB.Driver;

namespace DataAccessLayer.Owners.Logictics.Customers.Customer
{
    internal class CustomerBal: ICustomerBal
    {
        readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public CustomerBal(IStringLocalizer<Resource> localizer, IRepositoryWrapper repository)
        {
            this.repository = repository;
            this.localizer = localizer;
        }

        public async Task<Response> Get(int pageIndex=1,int pageSize=10)
        {
            var response = new Response { HttpStatusCode = 200 };


            var filter = Builders<CustomerModel>.Filter.Empty;
            var customertList = await repository.CustomerRepository.Page(filter, null, pageIndex,pageSize);
            if (customertList != null && customertList.List!=null && customertList.List.Any())
            {
                response.Model = customertList;
                response.HttpStatusCode = 200;
            }
            return response;
        }
        public async Task<Response> Post(CustomerModel doc)
        {
            var response = new Response { HttpStatusCode = 200 };
            var _id = doc._id;
            var ids = await repository.CustomerRepository.SequenceIds();
            if (ids._id != null && ids._id != "")
            {
                doc._key = ids._key;
                doc._rev = ids._rev;
            }
            var post = await repository.CustomerRepository.Post(doc);
            if (post != null && post._id.ToString() != "" && !post._id.ToString().Equals(_id))
            {
                response.Model = post._id;
                response.HttpStatusCode = 200;
                return response;
            }
            return response;
        }
    }
}
