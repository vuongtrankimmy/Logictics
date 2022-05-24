using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.OwnerModels.CRM.Management.Base
{

    //public interface ICreatorBaseModel
    //{
    //    public string CreatorName { get; set; }
    //    public string CreatorAcronymn { get; set; }
    //    public string CreatorAvatar { get; set; }
    //    public string CreateDate { get; set; }
    //}
    //public interface IBaseModel
    //{
    //    public ObjectId _id { get; set; }
    //    public long _key { get; set; }
    //    public string _rev { get; set; }
    //}

    [BsonIgnoreExtraElements]
    public abstract class BaseModel: CreatorBaseModel
    {
        //[BsonRequired]
        [BsonElement("_id")]
        //[JsonConverter(typeof(ObjectIdConverter))]
        [BsonRepresentation(BsonType.ObjectId)]
        //[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonIgnoreIfNull]
        public string _id { get; set; }
        [BsonIgnoreIfNull]
        //[BsonRequired]
        public long _key { get; set; }
        [BsonIgnoreIfNull]
        //[BsonRequired]
        public string _rev { get; set; }
    }

    [BsonIgnoreExtraElements]
    public abstract class CreatorBaseModel
    {
        [JsonProperty("User_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string User_id { get; set; }
        [JsonProperty("Location_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Location_id { get; set; }
        [JsonProperty("Creator", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Creator { get; set; }
        [JsonProperty("CreatorName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CreatorName { get; set; }
        [JsonProperty("CreatorAcronymn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CreatorAcronymn { get; set; }
        [JsonProperty("CreatorAvatar", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CreatorAvatar { get; set; }
        [JsonProperty("CreateDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CreateDate { get; set; }
        [JsonProperty("UpdateDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string UpdateDate { get; set; }
        [JsonProperty("StatusName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StatusName { get; set; }
        [JsonProperty("StatusColor", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StatusColor { get; set; }
        [JsonProperty("StatusAcronymn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string StatusAcronymn { get; set; }
    }

    public abstract class BaseFunctionModel
    {
        public string Language { get; set; } = "vi-VN";
    }

    internal class ObjectIdConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return typeof(ObjectId).IsAssignableFrom(objectType);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ObjectId).IsAssignableFrom(objectType);
            //return true;
        }
    }
}
