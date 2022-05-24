using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Products.ProductGroup;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Products.ProductGroup;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Products.ProductGroup
{
    public class ProductGroupRepository : RepositoryBase<ProductGroupModel>, IProductGroupRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.ProductsGroup.ToString();
        public ProductGroupRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<ProductGroupModel>> Page(FilterDefinition<ProductGroupModel> filter = null, SortDefinition<ProductGroupModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<ProductGroupModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<ProductGroupModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<ProductGroupModel>> ListByLimit(FilterDefinition<ProductGroupModel> filter = null, SortDefinition<ProductGroupModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<ProductGroupModel> Single(FilterDefinition<ProductGroupModel> filter = null, SortDefinition<ProductGroupModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<ProductGroupModel> SingleById(string _id) => await query.SingleById<ProductGroupModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public new async Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<ProductGroupModel> Post(ProductGroupModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds(string database, string table) => await query.SequenceIds(database, table);
    }
}
