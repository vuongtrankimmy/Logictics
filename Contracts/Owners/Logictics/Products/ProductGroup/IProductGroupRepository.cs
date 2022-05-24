using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Products.ProductGroup;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Products.ProductGroup
{
    public interface IProductGroupRepository : IRepositoryBase<ProductGroupModel>
    {
        Task<TEntityPaging<ProductGroupModel>> Page(FilterDefinition<ProductGroupModel> filter = null, SortDefinition<ProductGroupModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<ProductGroupModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<ProductGroupModel> sort = null, int limit = 0);
        Task<List<ProductGroupModel>> ListByLimit(FilterDefinition<ProductGroupModel> filter = null, SortDefinition<ProductGroupModel> sort = null, int limit = 0);
        Task<ProductGroupModel> Single(FilterDefinition<ProductGroupModel> filter = null, SortDefinition<ProductGroupModel> sort = null, int limit = 0);
        Task<ProductGroupModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<ProductGroupModel> Post(ProductGroupModel doc);
        new Task<IndexManagementModel> SequenceIds(string database, string table);
    }
}
