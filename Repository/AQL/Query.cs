using Contracts.AQL;
using Entities.Enumerations.Databases;
using Entities.Enumerations.Tables;
using Entities.Extensions.Response;
using Entities.OwnerModels.CRM.Management.Index;
using Helpers.encrypt.crc32;
using Helpers.Helper.Convert;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AQL
{
    public class Query : IQuery
    {
        private readonly IMongoClient _mongoClient;
        public Query(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }
        public async Task<TEntityPaging<TEntity>> Page<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int pageIndex = 0, int pageSize = 0)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            filter ??= Builders<TEntity>.Filter.Empty;
            var find=  collection.Find(filter);
            var itemTotal =await find.CountDocumentsAsync();
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            pageSize = pageSize > 0 ? pageSize : 10;
            var limit = (pageIndex - 1) * pageSize;
            var total=decimal.Parse(itemTotal.ToString());
            var pageTotal =Math.Ceiling((total / pageSize));
            if (sort != null)
            {
                find.Sort(sort);
            }
            if (limit > 0)
            {
                find.Skip(limit);
            }
            if (pageSize > 0)
            {
                find.Limit(pageSize);
            }
            var itemList = await find.ToListAsync();            
            var returnItemTotal = itemList.Count;
            var tEntityPaging = new TEntityPaging<TEntity>
            {
                PageIndex= pageIndex,
                PageSize=pageSize,
                PageTotal=pageTotal,
                ItemTotal=itemTotal,
                ReturnItemTotal= returnItemTotal,
                List =itemList
            };
            return tEntityPaging;
        }
        public async Task<List<TEntity>> GetInList<TEntity>(string database, string table, string fieldIn, string strInList = null, SortDefinition<TEntity> sort = null, int limit = 0)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            var inList = strInList != null && strInList != "" ? ConvertHelper.Deserialize<List<string>>(strInList) : null;            
            var filter = Builders<TEntity>.Filter.In(fieldIn, inList);
            var condition = collection.Find(filter);
            if (sort != null)
            {
                condition.Sort(sort);
            }
            if (limit > 0)
            {
                condition.Limit(limit);
            }
           return await condition.ToListAsync();
        }

        public async Task<List<TEntity>> GetInListByType<TEntity>(string database, string table, string fieldIn, IEnumerable<ObjectId> InList = null, SortDefinition<TEntity> sort = null, int limit = 0)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);            
            var filter = Builders<TEntity>.Filter.In(fieldIn, InList);
            var condition = collection.Find(filter);
            if (sort != null)
            {
                condition.Sort(sort);
            }
            if (limit > 0)
            {
                condition.Limit(limit);
            }
            return await condition.ToListAsync();
        }

        public async Task<List<TEntity>> GetInListById<TEntity>(string database, string table, string fieldIn, IEnumerable<int> InList = null, SortDefinition<TEntity> sort = null, int limit = 0)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            var filter = Builders<TEntity>.Filter.In(fieldIn, InList);
            var condition = collection.Find(filter);
            if (sort != null)
            {
                condition.Sort(sort);
            }
            if (limit > 0)
            {
                condition.Limit(limit);
            }
            return await condition.ToListAsync();
        }

        public async Task<List<TEntity>> GetInListBySingleId<TEntity>(string database, string table, string fieldIn, object fieldEq, SortDefinition<TEntity> sort = null, int limit = 0)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            var filter = Builders<TEntity>.Filter.Eq(fieldIn, fieldEq);
            var condition = collection.Find(filter);
            if (sort != null)
            {
                condition.Sort(sort);
            }
            if (limit > 0)
            {
                condition.Limit(limit);
            }
            return await condition.ToListAsync();
        }

        public async Task<List<TEntity>> ListByLimit<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int limit = 0)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            filter ??= Builders<TEntity>.Filter.Empty;
            var condition = collection.Find(filter);
             if (sort != null)
            {
                condition.Sort(sort);
            }
            if (limit > 0)
            {
                condition.Limit(limit);
            }
            return await condition.ToListAsync();
        }
        public async Task<TEntity> Single<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null, SortDefinition<TEntity> sort = null, int limit = 0)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            return await collection.Find(filter).Sort(sort).Limit(limit).FirstOrDefaultAsync();
        }
        public async Task<TEntity> SingleById<TEntity>(string database, string table, string _id)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            var filter = Builders<TEntity>.Filter.And(Builders<TEntity>.Filter.Eq("_id",ObjectId.Parse(_id)));
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<long> TotalItem<TEntity>(string database, string table, FilterDefinition<TEntity> filter = null)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            return await collection.Find(filter).CountDocumentsAsync();
        }
        public async Task<bool> Delete<TEntity>(string database, string table, string _id)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            var filter = Builders<TEntity>.Filter.And(Builders<TEntity>.Filter.Eq("_id", _id));
            var result = await collection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        public async Task<bool> Put<TEntity>(string database, string table, FilterDefinition<TEntity> filter,UpdateDefinition<TEntity> updateDefinition)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table);
            var result = await collection.UpdateOneAsync(filter, updateDefinition);
            return result.IsAcknowledged;
        }

        public async Task<TEntity> Post<TEntity>(string database, string table, TEntity doc)
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<TEntity>(table.ToString());
            await collection.InsertOneAsync(doc);
            return doc;
        }

        public async Task<IndexManagementModel> SequenceIds(string database, string table,string prefix="")
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<IndexManagement>(TablesManagement.ManagementIndex.ToString());

            var colExists = db.ListCollectionNames().ToList().Contains(TablesManagement.ManagementIndex.ToString());
            var field_id = table.ToString() + (prefix != "" ? "_" + prefix : "") + "_id";
            var field_value = "Value";
            if (colExists.Equals(false))
            {
                db.CreateCollection(TablesManagement.ManagementIndex.ToString());
            }
            var filter = Builders<IndexManagement>.Filter.And(Builders<IndexManagement>.Filter.Eq("_id", field_id));
            var tableIndex = await collection.FindAsync(filter);
            long updateValue;
            if (tableIndex == null || !tableIndex.Any())
            {
                var indexManagement = new IndexManagement
                {
                    _id = field_id
                };
                await collection.InsertOneAsync(indexManagement);
                updateValue = indexManagement.Value ?? 1;
            }
            else
            {
                var updates = Builders<IndexManagement>.Update.Inc(field_value, 1);
                var update = await collection.FindOneAndUpdateAsync(filter, updates);
                updateValue = update.Value + 1 ?? 1;
            }
            var _key = updateValue;
            var _id = table + "/" + _key;
            //var _rev = Convert.ToBase64String(Encoding.UTF8.GetBytes(_id + _key));
            //Encrypt encrypt = new();
            var _rev = Encrypt.CRC32(_id + _key);
            var indexManagementModel = new IndexManagementModel
            {
                _id = _id,
                _key = _key,
                _rev = _rev
            };
            return indexManagementModel;
        }

        public async Task<IndexManagementModel> SequenceIdsGet(string database, string table, string prefix = "")
        {
            var db = _mongoClient.GetDatabase(database);
            var collection = db.GetCollection<IndexManagement>(TablesManagement.ManagementIndex.ToString());

            var colExists = db.ListCollectionNames().ToList().Contains(TablesManagement.ManagementIndex.ToString());
            var field_id = table.ToString() + (prefix != "" ? "_" + prefix : "") + "_id";
            if (colExists.Equals(false))
            {
                db.CreateCollection(TablesManagement.ManagementIndex.ToString());
            }
            var filter = Builders<IndexManagement>.Filter.And(Builders<IndexManagement>.Filter.Eq("_id", field_id));
            var tableIndexList = await collection.Find(filter).ToListAsync();
            long value = 0;
            if (tableIndexList != null && tableIndexList.Any())
            {
                var tableIndex = tableIndexList.FirstOrDefault();
                value = tableIndex.Value??0;
            }
            var _key = value + 1;
            var _id = table + "/" + _key;
            var _rev = Encrypt.CRC32(_id + _key);
            var indexManagementModel = new IndexManagementModel
            {
                _id = _id,
                _key = _key,
                _rev = _rev
            };
            return indexManagementModel;
        }
    }
}
