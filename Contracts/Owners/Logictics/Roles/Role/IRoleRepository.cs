﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryBase;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Entities.OwnerModels.Logictics.Roles.Role;
using MongoDB.Driver;

namespace Contracts.Owners.Logictics.Roles.Role
{
    public interface IRoleRepository : IRepositoryBase<RoleModel>
    {
        Task<TEntityPaging<RoleModel>> Page(FilterDefinition<RoleModel> filter = null, SortDefinition<RoleModel> sort = null, int page = 0, int pageSize = 0);
        Task<List<RoleModel>> GetInList(string fieldIn, string strInList = null, SortDefinition<RoleModel> sort = null, int limit = 0);
        Task<List<RoleModel>> ListByLimit(FilterDefinition<RoleModel> filter = null, SortDefinition<RoleModel> sort = null, int limit = 0);
        Task<RoleModel> Single(FilterDefinition<RoleModel> filter = null, SortDefinition<RoleModel> sort = null, int limit = 0);
        Task<RoleModel> SingleById(string _id);
        Task<long> TotalItem<TEntity>(FilterDefinition<TEntity> filter = null);
        Task<bool> Delete<TEntity>(string _id);
        new Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        Task<RoleModel> Post(RoleModel doc);
        new Task<IndexManagementModel> SequenceIds(string database, string table);
    }
}
