using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Products.Product;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Products.Product;
using MongoDB.Bson;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Products.Product
{
    public class ProductRepository : RepositoryBase<ProductModel>, IProductRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.Products.ToString();
        public ProductRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<ProductViewModel>> Page(FilterDefinition<ProductViewModel> filter = null, SortDefinition<ProductViewModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<ProductViewModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<ProductViewModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<ProductViewModel>> GetInListByType(string fieldIn, IEnumerable<ObjectId> inList = null, SortDefinition<ProductViewModel> sort = null, int limit = 0) => await query.GetInListByType(database, table, fieldIn, inList, sort, limit);
        public async Task<List<ProductModel>> GetInListById(string fieldIn, IEnumerable<int> inList = null, SortDefinition<ProductModel> sort = null, int limit = 0) => await query.GetInListById(database, table, fieldIn, inList, sort, limit);
        public async Task<List<ProductModel>> ListByLimit(FilterDefinition<ProductModel> filter = null, SortDefinition<ProductModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<List<ProductChooseModel>> ListChooseByLimit(FilterDefinition<ProductChooseModel> filter = null, SortDefinition<ProductChooseModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<ProductModel> Single(FilterDefinition<ProductModel> filter = null, SortDefinition<ProductModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<ProductModel> SingleById(string _id) => await query.SingleById<ProductModel>(database, table, _id);
        public async Task<ProductFormModel> SingleFormById(string _id) => await query.SingleById<ProductFormModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete(string _id) => await query.Delete<ProductModel>(database, table, _id);
        public async Task<bool> Put<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<ProductModel> Post(ProductModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds(string prefix = "") => await query.SequenceIds(database, table,prefix);
        public new async Task<IndexManagementModel> SequenceIdsGet(string prefix = "") => await query.SequenceIdsGet(database, table, prefix);
    }
}
