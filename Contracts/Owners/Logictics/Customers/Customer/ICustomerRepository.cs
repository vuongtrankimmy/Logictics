using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Customers.Customer;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Customers.Customer
{
    public interface ICustomerRepository : IRepositoryBase<CustomerModel>
    {
        Task<TEntityPaging<CustomerModel>> Page(FilterDefinition<CustomerModel> filter = null, SortDefinition<CustomerModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<CustomerModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<CustomerModel> sort = null, int limit = 0);
        Task<List<CustomerModel>> ListByLimit(FilterDefinition<CustomerModel> filter = null, SortDefinition<CustomerModel> sort = null, int limit = 0);
        Task<CustomerModel> Single(FilterDefinition<CustomerModel> filter = null, SortDefinition<CustomerModel> sort = null, int limit = 0);
        Task<CustomerModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<CustomerModel> Post(CustomerModel doc);
        Task<IndexManagementModel> SequenceIds();
    }
}
