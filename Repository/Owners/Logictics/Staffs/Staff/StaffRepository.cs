using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.AQL;
using Contracts.Owners.Logictics.Staffs.Staff;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Staffs.Staff;
using MongoDB.Bson;
using MongoDB.Driver;
using Repository.RepositoryBase;

namespace Repository.Owners.Logictics.Staffs.Staff
{
    public class StaffRepository : RepositoryBase<StaffModel>, IStaffRepository
    {
        readonly IQuery query;
        private readonly string database = Databases.Logictics.ToString();
        private readonly string table = Tables.Staffs.ToString();
        public StaffRepository(IQuery query) : base(query)
        {
            this.query = query;
        }
        public async Task<TEntityPaging<StaffViewModel>> Page(FilterDefinition<StaffViewModel> filter = null, SortDefinition<StaffViewModel> sort = null, int page = 0, int pageSize = 0) => await query.Page(database, table, filter, sort, page, pageSize);
        public async Task<List<StaffModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<StaffModel> sort = null, int limit = 0) => await query.GetInList(database, table, fieldIn, strInList, sort, limit);
        public async Task<List<StaffModel>> GetInListByType(string fieldIn, IEnumerable<ObjectId> inList= null, SortDefinition<StaffModel> sort = null, int limit = 0) => await query.GetInListByType(database, table, fieldIn, inList, sort, limit);
        public async Task<List<StaffModel>> GetInListById(string fieldIn, IEnumerable<int> inList = null, SortDefinition<StaffModel> sort = null, int limit = 0) => await query.GetInListById(database, table, fieldIn, inList, sort, limit);
        public async Task<List<StaffModel>> ListByLimit(FilterDefinition<StaffModel> filter = null, SortDefinition<StaffModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<List<StaffViewModel>> ListViewByLimit(FilterDefinition<StaffViewModel> filter = null, SortDefinition<StaffViewModel> sort = null, int limit = 0) => await query.ListByLimit(database, table, filter, sort, limit);
        public async Task<StaffModel> Single(FilterDefinition<StaffModel> filter = null, SortDefinition<StaffModel> sort = null, int limit = 0) => await query.Single(database, table, filter, sort, limit);
        public async Task<StaffViewModel> SingleById(string _id) => await query.SingleById<StaffViewModel>(database, table, _id);
        public async Task<StaffFormModel> SingleFormById(string _id) => await query.SingleById<StaffFormModel>(database, table, _id);
        public async Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null) => await query.TotalItem(database, table, filter);
        public async Task<bool> Delete<TEntity>(string _id) => await query.Delete<TEntity>(database, table, _id);
        public async Task<bool> Put<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition) => await query.Put(database, table, filter, updateDefinition);
        public async Task<StaffModel> Post(StaffModel doc) => await query.Post(database, table, doc);
        public new async Task<IndexManagementModel> SequenceIds(string database, string table) => await query.SequenceIds(database, table);
    }
}
