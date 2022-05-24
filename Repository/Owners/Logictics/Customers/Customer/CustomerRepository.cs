using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Customers.Customer;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Customers.Customer;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Customers.Customer
{
    public class CustomerRepository : RepositoryBase<CustomerModel>, ICustomerRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.Customers.ToString();
        public CustomerRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<CustomerModel>> Page(FilterDefinition<CustomerModel> filter = null, SortDefinition<CustomerModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<CustomerModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<CustomerModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<CustomerModel>> ListByLimit(FilterDefinition<CustomerModel> filter = null, SortDefinition<CustomerModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<CustomerModel> Single(FilterDefinition<CustomerModel> filter = null, SortDefinition<CustomerModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<CustomerModel> SingleById(string _id) => await query.SingleById<CustomerModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public new async Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<CustomerModel> Post(CustomerModel doc) => await query.Post(database, table, doc);
        public async Task<IndexManagementModel> SequenceIds() => await query.SequenceIds(database, table);
    }
}
