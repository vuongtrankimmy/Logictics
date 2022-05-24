using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Products.ProductAllocationRequired;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Products.ProductAllocationRequired;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Products.ProductAllocationRequired
{
    public class ProductAllocationRequiredRepository: RepositoryBase<ProductAllocationRequiredModel>, IProductAllocationRequiredRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.ProductsAllocationRequired.ToString();
        public ProductAllocationRequiredRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<ProductAllocationRequiredViewModel>> Page(FilterDefinition<ProductAllocationRequiredViewModel> filter = null, SortDefinition<ProductAllocationRequiredViewModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<ProductAllocationRequiredViewModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<ProductAllocationRequiredViewModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<ProductAllocationRequiredViewModel>> ListByLimit(FilterDefinition<ProductAllocationRequiredViewModel> filter = null, SortDefinition<ProductAllocationRequiredViewModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<ProductAllocationRequiredModel> Single(FilterDefinition<ProductAllocationRequiredModel> filter = null, SortDefinition<ProductAllocationRequiredModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<ProductAllocationRequiredModel> SingleById(string _id) => await query.SingleById<ProductAllocationRequiredModel>(database, table, _id);
        public async Task<List<ProductAllocationRequiredViewModel>> GetInListBySingleId(string fieldIn,object fieldEq, SortDefinition<ProductAllocationRequiredViewModel> sort = null, int limit = 0) => await query.GetInListBySingleId(database, table, fieldIn, fieldEq, sort, limit);
        public async Task<ProductAllocationRequiredFormModel> SingleFormById(string _id) => await query.SingleById<ProductAllocationRequiredFormModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public new async Task<bool> Put<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<ProductAllocationRequiredModel> Post(ProductAllocationRequiredModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds() => await query.SequenceIds(database, table);
    }
}
