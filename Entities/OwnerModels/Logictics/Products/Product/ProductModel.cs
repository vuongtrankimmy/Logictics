using System.Collections.Generic;
using System.Web.Mvc;
using Entities.OwnerModels.CRM.Management.Base;
using Entities.OwnerModels.Logictics.Systems.CodesTypes.CodesType;
using Entities.OwnerModels.Logictics.Systems.Products.ProductType;
using Entities.OwnerModels.Logictics.Systems.Units.Unit;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entities.OwnerModels.Logictics.Products.Product
{
    [BsonIgnoreExtraElements]

    public partial class ProductModel : ProductInherit
    {

    }

    [BsonIgnoreExtraElements]
    public partial class ProductViewModel : ProductInherit
    {
        [JsonProperty("UnitName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string UnitName { get; set; }
        [JsonProperty("ProductTypeName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductTypeName { get; set; }
        [JsonProperty("CodesTypeName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CodesTypeName { get; set; }
    }
    [BsonIgnoreExtraElements]
    public partial class ProductInherit : ProductBase
    {
        [JsonProperty("CodeTypeId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? CodeTypeId { get; set; }       

        [JsonProperty("SellPrice", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SellPrice { get; set; }
        [JsonProperty("SellPriceFormat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string SellPriceFormat { get { return SellPrice?.ToString("#,###"); } }

        [JsonProperty("ComparePrice", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ComparePrice { get; set; }
        [JsonProperty("ComparePriceFormat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ComparePriceFormat { get { return ComparePrice?.ToString("#,###"); } }

        [JsonProperty("QuotaQuantity", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? QuotaQuantity { get; set; }
        [AllowHtml]
        [JsonProperty("Description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("Note", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }
        [JsonProperty("Bookmark", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? Bookmark { get; set; }
    }

    [BsonIgnoreExtraElements]
    public partial class ProductUseBase : BaseModel
    {
        [JsonProperty("ProductCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductCode { get; set; }
        [JsonProperty("ProductName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductName { get; set; }

        [JsonProperty("ProductNameAnsi", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductNameAnsi { get; set; }

        [JsonProperty("ProductNameAcronymn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProductNameAcronymn { get; set; }

        [JsonProperty("Color", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("Url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty("Sku", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Sku { get; set; }
    }

    [BsonIgnoreExtraElements]
    public partial class ProductBase : ProductUseBase
    {
        [JsonProperty("ProductId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? ProductId { get; set; }

        [JsonProperty("RefId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RefId { get; set; }

        [JsonProperty("Visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        [JsonProperty("UnitId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? UnitId { get; set; }

        [JsonProperty("ProductTypeId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? ProductTypeId { get; set; }
        [JsonProperty("IsPublish", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsPublish { get; set; }
        [JsonProperty("ispublishview", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ispublishview { get { return IsPublish ? "" : "checked"; } }
    }

    [BsonIgnoreExtraElements]
    public partial class ProductFormModel : ProductViewModel
    {
        [JsonProperty("ProductTypeList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductTypeModel> ProductTypeList { get; set; }
        [JsonProperty("UnitList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<UnitModel> UnitList { get; set; }
        public List<CodesTypeModel> CodeTypeList { get; set; }
        public List<Dictionary<string, object>> HeaderList { get; set; }
        public List<Dictionary<string, object>> BodyList { get; set; }
        public int? MaterialProductTotal { get; set; }

    }
    [BsonIgnoreExtraElements]
    public partial class ProductChooseModel : ProductUseBase
    {
        [JsonProperty("Default", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; }
        [JsonProperty("select", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string select { get; set; }
    }
}
