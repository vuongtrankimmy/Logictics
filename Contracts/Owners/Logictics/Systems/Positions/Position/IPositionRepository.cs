using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Systems.Positions.Position;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Systems.Positions.Position
{
    public interface IPositionRepository : IRepositoryBase<PositionModel>
    {
        Task<TEntityPaging<PositionModel>> Page(FilterDefinition<PositionModel> filter = null, SortDefinition<PositionModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<PositionModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<PositionModel> sort = null, int limit = 0);
        Task<List<PositionModel>> ListByLimit(FilterDefinition<PositionModel> filter = null, SortDefinition<PositionModel> sort = null, int limit = 0);
        Task<PositionModel> Single(FilterDefinition<PositionModel> filter = null, SortDefinition<PositionModel> sort = null, int limit = 0);
        Task<PositionModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<PositionModel> Post(PositionModel doc);
        new Task<IndexManagementModel> SequenceIds(string prefix = "");
        Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "");
    }
}
