using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Products.Product;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Products.Product
{
    public interface IProductRepository : IRepositoryBase<ProductModel>
    {
        Task<TEntityPaging<ProductViewModel>> Page(FilterDefinition<ProductViewModel> filter = null, SortDefinition<ProductViewModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<ProductViewModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<ProductViewModel> sort = null, int limit = 0);
        Task<List<ProductViewModel>> GetInListByType(string fieldIn, IEnumerable<ObjectId> inList = null, SortDefinition<ProductViewModel> sort = null, int limit = 0);
        Task<List<ProductModel>> GetInListById(string fieldIn, IEnumerable<int> inList = null, SortDefinition<ProductModel> sort = null, int limit = 0);
        Task<List<ProductModel>> ListByLimit(FilterDefinition<ProductModel> filter = null, SortDefinition<ProductModel> sort = null, int limit = 0);
        Task<List<ProductChooseModel>> ListChooseByLimit(FilterDefinition<ProductChooseModel> filter = null, SortDefinition<ProductChooseModel> sort = null, int limit = 0);
        Task<ProductModel> Single(FilterDefinition<ProductModel> filter = null, SortDefinition<ProductModel> sort = null, int limit = 0);
        Task<ProductModel> SingleById(string _id);
        Task<ProductFormModel> SingleFormById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete(string _id);
        Task<bool> Put<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<ProductModel> Post(ProductModel doc);
        new Task<IndexManagementModel> SequenceIds(string prefix = "");
        new Task<IndexManagementModel> SequenceIdsGet(string prefix = "");
    }
}
