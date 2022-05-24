using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.CRM.Management.Base;
using Entities.OwnerModels.Logictics.Products.Product;
using Entities.OwnerModels.Logictics.Systems.CodesTypes.CodesType;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Products.ProductAllocationRequired
{

    [BsonIgnoreExtraElements]
    public partial class ProductAllocationRequiredViewModel : ProductAllocationRequiredModel
    {
        [JsonProperty("ProductName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductName { get; set; }
        [JsonProperty("CodesTypeName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CodesTypeName { get; set; }
        [JsonProperty("ProductCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductCode { get; set; } = "";
        [JsonProperty("Url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty("MaterialTotal", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MaterialTotal { get; set; }
        [JsonProperty("MaterialTop", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MaterialTop { get; set; }

    }
    [BsonIgnoreExtraElements]
    public partial class ProductAllocationRequiredModel: ProductAllocationRequiredBaseModel
    {
       
        [JsonProperty("ProductAllocationRequiredCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductAllocationRequiredCode { get; set; }

        [JsonProperty("InvoiceNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceNumber { get; set; }       

        [JsonProperty("ProductList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductList> ProductList { get; set; }
    }

    [BsonIgnoreExtraElements]
    public partial class ProductAllocationRequiredBaseModel : BaseModel
    {
        [JsonProperty("Product_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Product_id { get; set; }
        [JsonProperty("CodeTypeId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? CodeTypeId { get; set; }
        [JsonProperty("IsPublish", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsPublish { get; set; }
        [JsonProperty("IsPublishView", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string IsPublishView { get { return IsPublish.Equals(true) ? "checked" : ""; } }

        [JsonProperty("Quantity", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? Quantity { get; set; }

        [JsonProperty("Loss", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Loss { get; set; }

        [JsonProperty("SourceChooseTypeId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? SourceChooseTypeId { get; set; }
    }

    [BsonIgnoreExtraElements]
    public partial class ProductList
    {
        [JsonProperty("Product_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Product_id { get; set; }
        [JsonProperty("ProductCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductCode { get; set; }
        [JsonProperty("ProductName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductName { get; set; }

        [JsonProperty("QuantityUsed", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? QuantityUsed { get; set; }
        [JsonProperty("QuantityStock", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? QuantityStock { get; set; }
        [JsonProperty("QuantityBalance", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? QuantityBalance { get; set; }
    }

    [BsonIgnoreExtraElements]
    public partial class ProductAllocationRequiredFormModel : ProductAllocationRequiredViewModel
    {
        [JsonProperty("ProductChooseList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductChooseModel> ProductChooseList { get; set; }
        [JsonProperty("Product", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ProductChooseModel Product { get; set; }
        [JsonProperty("CodeTypeList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<CodesTypeModel> CodeTypeList { get; set; }

    }
}
