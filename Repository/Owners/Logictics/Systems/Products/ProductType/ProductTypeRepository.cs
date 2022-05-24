using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Systems.Products.ProductType;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.Products.ProductType;
using MongoDB.Bson;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Systems.Products.ProductType
{
    public class ProductTypeRepository : RepositoryBase<ProductTypeModel>, IProductTypeRepository
    {
        private readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.ProductsType.ToString();
        public ProductTypeRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<ProductTypeModel>> Page(FilterDefinition<ProductTypeModel> filter = null, SortDefinition<ProductTypeModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<ProductTypeModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<ProductTypeModel>> GetInListByType(string fieldIn, IEnumerable<ObjectId> inList = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0) => await query.GetInListByType(database, table, fieldIn, inList, sort, limit);
        public async Task<List<ProductTypeModel>> GetInListById(string fieldIn, IEnumerable<int> inList = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0) => await query.GetInListById(database, table, fieldIn, inList, sort, limit);
        public async Task<List<ProductTypeModel>> ListByLimit(FilterDefinition<ProductTypeModel> filter = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<ProductTypeModel> Single(FilterDefinition<ProductTypeModel> filter = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<ProductTypeModel> SingleById(string _id) => await query.SingleById<ProductTypeModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public async Task<bool> Put<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<ProductTypeModel> Post(ProductTypeModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds(string prefix="") => await query.SequenceIds(database, table,prefix);
        public new async Task<IndexManagementModel> SequenceIdsGet(string prefix = "") => await query.SequenceIdsGet(database, table, prefix);
    }
}
