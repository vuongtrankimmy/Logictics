using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Orders.Order;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Orders.Order
{
    public interface IOrderRepository : IRepositoryBase<OrderModel>
    {
        Task<TEntityPaging<OrderModel>> Page(FilterDefinition<OrderModel> filter = null, SortDefinition<OrderModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<OrderModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<OrderModel> sort = null, int limit = 0);
        Task<List<OrderModel>> ListByLimit(FilterDefinition<OrderModel> filter = null, SortDefinition<OrderModel> sort = null, int limit = 0);
        Task<OrderModel> Single(FilterDefinition<OrderModel> filter = null, SortDefinition<OrderModel> sort = null, int limit = 0);
        Task<OrderModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<OrderModel> Post(OrderModel doc);
        new Task<IndexManagementModel> SequenceIds(string database, string table);
    }
}
