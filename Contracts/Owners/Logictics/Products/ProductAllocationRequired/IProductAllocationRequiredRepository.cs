using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Products.ProductAllocationRequired;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Products.ProductAllocationRequired
{
    public interface IProductAllocationRequiredRepository: IRepositoryBase<ProductAllocationRequiredModel>
    {
        Task<TEntityPaging<ProductAllocationRequiredViewModel>> Page(FilterDefinition<ProductAllocationRequiredViewModel> filter = null, SortDefinition<ProductAllocationRequiredViewModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<ProductAllocationRequiredViewModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<ProductAllocationRequiredViewModel> sort = null, int limit = 0);
        Task<List<ProductAllocationRequiredViewModel>> ListByLimit(FilterDefinition<ProductAllocationRequiredViewModel> filter = null, SortDefinition<ProductAllocationRequiredViewModel> sort = null, int limit = 0);
        Task<ProductAllocationRequiredModel> Single(FilterDefinition<ProductAllocationRequiredModel> filter = null, SortDefinition<ProductAllocationRequiredModel> sort = null, int limit = 0);
        Task<List<ProductAllocationRequiredViewModel>> GetInListBySingleId(string fieldIn, object fieldEq, SortDefinition<ProductAllocationRequiredViewModel> sort = null, int limit = 0);
        Task<ProductAllocationRequiredModel> SingleById(string _id);
        Task<ProductAllocationRequiredFormModel> SingleFormById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<ProductAllocationRequiredModel> Post(ProductAllocationRequiredModel doc);
        new Task<IndexManagementModel> SequenceIds();
    }
}
