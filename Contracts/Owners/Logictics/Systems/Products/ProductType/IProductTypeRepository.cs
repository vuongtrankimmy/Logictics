using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.Products.ProductType;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Systems.Products.ProductType
{
    public interface IProductTypeRepository : IRepositoryBase<ProductTypeModel>
    {
        Task<TEntityPaging<ProductTypeModel>> Page(FilterDefinition<ProductTypeModel> filter = null, SortDefinition<ProductTypeModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<ProductTypeModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0);
        Task<List<ProductTypeModel>> GetInListByType(string fieldIn, IEnumerable<ObjectId> strInList = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0);
        Task<List<ProductTypeModel>> GetInListById(string fieldIn, IEnumerable<int> strInList = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0);
        Task<List<ProductTypeModel>> ListByLimit(FilterDefinition<ProductTypeModel> filter = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0);
        Task<ProductTypeModel> Single(FilterDefinition<ProductTypeModel> filter = null, SortDefinition<ProductTypeModel> sort = null, int limit = 0);
        Task<ProductTypeModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<ProductTypeModel> Post(ProductTypeModel doc);
        new Task<IndexManagementModel> SequenceIds(string prefix = "");
        new Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "");
    }
}
