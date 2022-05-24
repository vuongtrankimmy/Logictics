using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BusinessLogicLayer.Owners.Logictics.Products.Product;
using Contracts.RepositoryWrapper;
using Entities.ExtendedModels.Localize;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.Extensions.NullDySession;
using Entities.Extensions.Response;
using Entities.Extensions.Tokens;
using Entities.OwnerModels.Logictics.Products.Product;
using Entities.OwnerModels.Logictics.Staffs.Staff;
using Entities.OwnerModels.Logictics.Systems.CodesTypes.CodesType;
using Entities.OwnerModels.Logictics.Systems.Products.ProductType;
using Entities.OwnerModels.Logictics.Systems.Units.Unit;
using Helpers.Helper.Colors;
using Helpers.Helper.Convert;
using Helpers.Helper.Paging;
using Helpers.Helper.UnicodeToAnsi;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLayer.Owners.Logictics.Products.Product
{
    public class ProductDal : IProductBal
    {
        readonly TokenView tokenView = NullDySession.TokenUser ?? new TokenView();
        readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public ProductDal(IStringLocalizer<Resource> localizer, IRepositoryWrapper repository)
        {
            this.repository = repository;
            this.localizer = localizer;
        }

        #region Paging
        /// <summary>
        /// Phân trang danh sách khách hàng
        /// 2022/04/04
        /// Trần Vương
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">Trang hiện tại hiển thị dữ liệu</param>
        /// <param name="pageSize">Tổng số dữ liệu hiển thị</param>
        /// <returns></returns>
        public async Task<Response> Paging(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            keyword ??= "";
            var bsonRegular = new BsonRegularExpression("*" + keyword + "*", "i");
            var isNumeric = long.TryParse(keyword, out long n);
            var filter =
                  Builders<ProductViewModel>.Filter.Eq("ProductCode", keyword)
                | Builders<ProductViewModel>.Filter.Eq("Sku", keyword)
                | Builders<ProductViewModel>.Filter.Eq("ProductId", isNumeric)
                | Builders<ProductViewModel>.Filter.Regex("ProductName", keyword)
                | Builders<ProductViewModel>.Filter.Regex("ProductNameAnsi", keyword)
                | Builders<ProductViewModel>.Filter.Regex("ProductNameAcronymn", keyword);
            var sort = Builders<ProductViewModel>.Sort.Descending("_id").Descending("Create");
            var productList = await repository.ProductRepository.Page(filter, sort, pageIndex, pageSize);
            var httpCodeStatus = 404;
            if (productList != null && productList.ItemTotal > 0)
            {
                var products = productList.List;
                #region Creator Id List
                var creatorIdList = products.Where(x => x.Creator != null && x.Creator != "").Select(x => ObjectId.Parse(x.Creator)).Distinct();
                var creatorList = new List<StaffModel>();
                if (creatorIdList != null && creatorIdList.Any())
                {
                    creatorList = await repository.StaffRepository.GetInListByType("_id", creatorIdList);
                }
                #endregion

                #region Product Type List
                var productTypeIdList = products.Where(x => x.ProductTypeId != null && x.ProductTypeId > 0).Select(x => x.ProductTypeId ?? 0).Distinct();
                var productTypeList = new List<ProductTypeModel>();
                if (productTypeIdList != null && productTypeIdList.Any())
                {
                    productTypeList = await repository.ProductTypeRepository.GetInListById("ProductTypeId", productTypeIdList);
                }
                #endregion

                #region Unit List
                var unitIdList = products.Where(x => x.UnitId != null && x.UnitId > 0).Select(x => x.UnitId ?? 0).Distinct();
                var unitList = new List<UnitModel>();
                if (unitIdList != null && unitIdList.Any())
                {
                    unitList = await repository.UnitRepository.GetInListById("UnitId", unitIdList);
                }
                #endregion

                #region CodesType List
                var codesTypeIdList = products.Where(x => x.CodeTypeId != null && x.CodeTypeId > 0).Select(x => x.CodeTypeId ?? 0).Distinct();
                var codesTypeList = new List<CodesTypeModel>();
                if (codesTypeIdList != null && codesTypeIdList.Any())
                {
                    codesTypeList = await repository.CodesTypeRepository.GetInListById("CodeTypeId", codesTypeIdList);
                }
                #endregion

                foreach (var product in products)
                {
                    var creator_id = product.Creator ?? "";
                    var productTypeId = product.ProductTypeId ?? 0;
                    var unitId = product.UnitId ?? 0;
                    var codeTypeId = product.CodeTypeId ?? 0;
                    if (creator_id != "")
                    {
                        if (creatorList != null && creatorList.Any())
                        {
                            var creator = creatorList.Where(x => x._id.Equals(creator_id)).FirstOrDefault();
                            if (creator != null)
                            {
                                product.CreatorName = creator.Nickname ?? creator.StaffName;
                                product.CreatorAvatar = creator.Avatar;
                                product.CreatorAcronymn = creator.StaffNameAcronymn;
                            }
                        }
                    }

                    if (productTypeId > 0)
                    {
                        var productType = productTypeList.Where(x => x.ProductTypeId.Equals(productTypeId)).FirstOrDefault();
                        if (productType != null)
                        {
                            product.ProductTypeName = productType.ProductTypeName;
                        }
                    }

                    if (unitId > 0)
                    {
                        var unit = unitList.Where(x => x.UnitId.Equals(unitId)).FirstOrDefault();
                        if (unit != null)
                        {
                            product.UnitName = unit.UnitName;
                        }
                    }

                    if (codeTypeId > 0)
                    {
                        var codesType = codesTypeList.Where(x => x.CodeTypeId.Equals(codeTypeId)).FirstOrDefault();
                        if (codesType != null)
                        {
                            product.CodesTypeName = codesType.CodeTypeName;
                            var prefix = codesType.CodeTypePrefix ?? "";
                            var numberFormat = codesType.CodeTypeNumber ?? 1;
                            var hyphen = codesType.CodeTypeHyphen ?? "";
                            var productCode = product.ProductCode.ToCode(numberFormat, prefix, hyphen);
                            product.ProductCode = productCode;
                        }
                    }
                }

                httpCodeStatus = 200;
                var page = PagingHelper.Get(keyword, pageIndex, pageSize, decimal.Parse(productList.ItemTotal.ToString()));
                productList.Pages = page;
                productList.Keyword = keyword;
            }
            var response = new Response
            {
                HttpStatusCode = httpCodeStatus,
                Model = productList
            };
            return response;
        }
        #endregion
        #region Get by _id
        /// <summary>
        /// Lấy thông tin chi tiết khách hàng theo _id
        /// 2022/04/04
        /// Trần Vương
        /// </summary>
        /// <param name="_id">Mã khách hàng theo ObjectId</param>
        /// <returns></returns>
        public async Task<Response> GetBy_id(string _id)
        {
            var httpCodeStatus = 404;
            var product = new ProductFormModel();
            var productTypeId = 0;
            var unitId = 0;
            var codeTypeId = 0;
            _id ??= "";
            if (_id != null && _id != "")
            {
                product = await repository.ProductRepository.SingleFormById(_id);
                if (product?._id != null && product._id.ToString() != "")
                {
                    productTypeId = product.ProductTypeId ?? 0;
                    unitId = product.UnitId ?? 0;
                    codeTypeId = product.CodeTypeId ?? 0;
                    httpCodeStatus = 200;
                }
            }
            else
            {
                httpCodeStatus = 200;
            }

            var select = "selected";
            var check = "checked";
            #region Product Type List
            var productTypeList = await repository.ProductTypeRepository.ListByLimit();
            if (productTypeList.Count > 0)
            {
                if (productTypeId > 0)
                {
                    var productType = productTypeList.Where(x => x.ProductTypeId.Equals(productTypeId)).FirstOrDefault();
                    if (productType != null)
                    {
                        product.ProductTypeName = productType.ProductTypeName;
                        productTypeList.ForEach(x => { x.Default = false; x.check = ""; });
                        productType.Default = true;
                        productType.check = check;
                    }
                }
                else
                {
                    var productType = productTypeList.FirstOrDefault();
                    if (productType != null)
                    {
                        product.ProductTypeName = productType.ProductTypeName;
                        productTypeList.ForEach(x => { x.Default = false; x.check = ""; });
                        productType.Default = true;
                        productType.check = check;
                    }
                }
            }
            var description = HttpUtility.HtmlDecode(product.Description);
            product.Description = description;
            product.ProductTypeList = productTypeList;
            #endregion
            #region Unit List
            var unitList = await repository.UnitRepository.ListByLimit();
            if (unitList.Count > 0)
            {
                if (unitId > 0)
                {
                    var unit = unitList.Where(x => x.UnitId.Equals(unitId)).FirstOrDefault();
                    if (unit != null)
                    {
                        product.UnitName = unit.UnitName;
                        unitList.ForEach(x => { x.Default = false; x.select = ""; });
                        unit.Default = true;
                        unit.select = select;
                    }
                }
                else
                {
                    var unit = unitList.FirstOrDefault();
                    if (unit != null)
                    {
                        product.UnitName = unit.UnitName;
                        unitList.ForEach(x => { x.Default = false; x.select = ""; });
                        unit.Default = true;
                        unit.select = select;
                    }
                }
            }
            product.UnitList = unitList;
            #endregion

            #region Code Type List
            var codesTypeFilter = Builders<CodesTypeModel>.Filter.Eq("CodeTypeGroupId", 2);//2: Product
            var codesTypeList = await repository.CodesTypeRepository.ListByLimit(codesTypeFilter);
            var productCodeTypeName = "";
            var productCode = "";
            if (codesTypeList.Count > 0)
            {
                var prefix = "";
                var numberFormat = 0;
                var hyphen = "";
                if (codeTypeId > 0)
                {
                    var codesType = codesTypeList.Where(x => x.CodeTypeId.Equals(codeTypeId)).FirstOrDefault();
                    if (codesType != null)
                    {
                        productCodeTypeName = codesType.CodeTypeName;
                        codesTypeList.ForEach(x => { x.Default = false; x.select = ""; });
                        codesType.Default = true;
                        codesType.select = select;
                        prefix = codesType.CodeTypePrefix;
                        numberFormat = codesType.CodeTypeNumber ?? 0;
                        hyphen = codesType.CodeTypeHyphen;
                    }
                }
                else
                {
                    var codesType = codesTypeList.FirstOrDefault();
                    if (codesType != null)
                    {
                        productCodeTypeName = codesType.CodeTypeName;
                        codesTypeList.ForEach(x => { x.Default = false; x.select = ""; });
                        codesType.Default = true;
                        codesType.select = select;
                        prefix = codesType.CodeTypePrefix;
                        numberFormat = codesType.CodeTypeNumber ?? 0;
                        hyphen = codesType.CodeTypeHyphen;
                    }
                }
                if (_id == "")
                {
                    long _key = 1;
                    var sequenIds = await repository.ProductRepository.SequenceIdsGet(prefix);//Chỗ này xử lý đúng thì xem xét lấy id cuối cùng của danh sách.
                    if (sequenIds != null && sequenIds._key > 0)
                    {
                        _key = sequenIds._key;
                    }
                    productCode = _key.ToString().ToCode(numberFormat, prefix, hyphen);
                }
                else
                {
                    productCode = product.ProductCode.ToCode(numberFormat, prefix, hyphen);
                }
                product.ProductCode = productCode;
            }
            product.CodesTypeName = productCodeTypeName;
            product.CodeTypeList = codesTypeList;
            #endregion

            //AllocationRequired=AR
            //ProductAllocationRequired=PAR
            var productARList = await repository.ProductAllocationRequiredRepository.GetInListBySingleId("Product_id", _id);
            if (productARList.Count > 0)
            {
                var productListFromPAR = (from productAR in productARList
                                          from productA in productAR.ProductList
                                          let product_id = productA.Product_id
                                          where product_id != null && product_id != ""
                                          select ObjectId.Parse(product_id)).Distinct().ToList();
                if (productListFromPAR.Count > 0)
                {
                    var product_idPARJson = ConvertHelper.Serialize(productListFromPAR);
                    var productInList = await repository.ProductRepository.GetInList("_id", product_idPARJson);

                    #region Header Allocation Required By Product
                    var allocationRequiredHeaderList = new List<Dictionary<string, object>>();
                    var headerList = new List<Dictionary<string, object>>();
                    var headerProductCodeList = new List<Dictionary<string, object>>();
                    var headerProductCodeDic = new Dictionary<string, object>();
                    var headerDic = new Dictionary<string, object>
                    {
                        {"Value","Mã yêu cầu" },
                        {"css","rowspan='3' style='vertical-align:middle'"}
                    };
                    headerList.Add(headerDic);
                    headerDic = new Dictionary<string, object>
                    {
                        {"Value","Ngày yêu cầu" },
                        {"css","rowspan='3' style='vertical-align:middle'"}
                    };
                    headerList.Add(headerDic);
                    headerDic = new Dictionary<string, object>
                    {
                        {"Value","Số lượng" },
                        {"css","rowspan='3' style='vertical-align:middle'"}
                    };
                    headerList.Add(headerDic);
                    headerDic = new Dictionary<string, object>
                    {
                        {"Value","Độ hao hụt" },
                        {"css","rowspan='3' style='vertical-align:middle'"}
                    };
                    headerList.Add(headerDic);
                    var productInShort = productInList.Select(x => new { x._id, x.ProductCode, x.ProductName }).Distinct();
                    product.MaterialProductTotal = productInShort.Count();
                    foreach (var productIn in productInShort)
                    {
                        var productName = productIn.ProductName;
                        headerDic = new Dictionary<string, object>
                        {
                            {"Value",productName},
                            {"css","scope='col'"}
                        };
                        headerList.Add(headerDic);

                        var productCodeIn = productIn.ProductCode;

                        headerProductCodeDic = new Dictionary<string, object>
                        {
                            {"Value",productCodeIn},
                            {"css","scope='col'"}
                        };
                        headerProductCodeList.Add(headerProductCodeDic);
                    }
                    var headerListDic = new Dictionary<string, object>
                    {
                        {"List",headerList }
                    };
                    allocationRequiredHeaderList.Add(headerListDic);
                    headerListDic = new Dictionary<string, object>
                    {
                        {"List",headerProductCodeList }
                    };
                    allocationRequiredHeaderList.Add(headerListDic);
                    #endregion

                    #region Body Allocation Required By Product
                    var allocationRequiredBodyList = new List<Dictionary<string, object>>();
                    foreach (var productAR in productARList)
                    {
                        var productARDic = new List<Dictionary<string, object>>();
                        var dic = new Dictionary<string, object>();
                        var invoiceNumber = productAR.InvoiceNumber;

                        dic = new Dictionary<string, object>
                        {
                            {"Value",invoiceNumber},
                            {"css","class='fw-bold'"}
                        };
                        productARDic.Add(dic);

                        var requestDate = productAR.CreateDate;
                        dic = new Dictionary<string, object>
                        {
                            {"Value",requestDate},
                            {"css","class='fw-light'"}
                        };
                        productARDic.Add(dic);
                        var quantity = productAR.Quantity?.IntToCurrency();

                        dic = new Dictionary<string, object>
                        {
                            {"Value",quantity},
                            {"css","class='fw-bold'"}
                        };
                        productARDic.Add(dic);
                        var loss = productAR.Loss;

                        dic = new Dictionary<string, object>
                        {
                            {"Value",loss},
                            {"css","class='text-danger'"}
                        };

                        productARDic.Add(dic);
                        var sourceChooseTypeId = productAR.SourceChooseTypeId;
                        var productListFromAR = productAR.ProductList;
                        if (productInShort.Any())
                        {
                            foreach (var productIn in productInShort)
                            {
                                var product_id = productIn._id;
                                var quantityUsed = 0;
                                if (productListFromAR.Count > 0)
                                {
                                    var productListFromARIn = productListFromAR.Where(x => x.Product_id.Equals(product_id)).FirstOrDefault();
                                    if (productListFromARIn != null)
                                    {
                                        quantityUsed = productListFromARIn.QuantityUsed ?? 0;
                                    }
                                }
                                var quantityText = quantityUsed.IntToCurrency();
                                var css = quantityUsed > 0 ? "" : "class='bg-gray-100'";
                                dic = new Dictionary<string, object>
                                {
                                    {"Value",quantityText},
                                    {"css",css}
                                };
                                productARDic.Add(dic);
                            }
                        }
                        var bodyListDic = new Dictionary<string, object>
                        {
                            {"List",productARDic }
                        };
                        allocationRequiredBodyList.Add(bodyListDic);
                    }
                    #endregion                    
                    product.HeaderList = allocationRequiredHeaderList;
                    product.BodyList = allocationRequiredBodyList;
                }
            }

            var response = new Response
            {
                HttpStatusCode = httpCodeStatus,
                Model = product
            };
            return response;
        }
        #endregion
        #region Post
        /// <summary>
        /// Insert
        /// 2022/04/04
        /// Trần Vương
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public async Task<Response> Post(ProductModel doc)
        {
            var response = new Response { HttpStatusCode = 200 };
            if (doc == null || doc.ProductName == null)
            {
                response.Message = localizer["doc"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            if (doc.ProductName == null || doc.ProductName == "")
            {
                response.Message = localizer["ProductName_blank"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            var createDate = ConvertHelper.Now();
            var filter = Builders<ProductModel>.Filter.Eq("ProductName", doc.ProductName);
            var productRepositoryList = await repository.ProductRepository.ListByLimit(filter, null, 10);
            if (productRepositoryList != null && productRepositoryList.Any())
            {
                var productRepository = productRepositoryList.Where(x => x.ProductName.Equals(doc.ProductName)).FirstOrDefault();
                if (productRepository != null)
                {
                    response.Message = String.Format(localizer["account_exist"].Value, doc.ProductName, localizer["lbl_productName"].Value);
                    response.HttpStatusCode = 204;
                    return response;
                }
            }
            var prefix = "";
            var codeTypeId = doc.CodeTypeId ?? 0;//Lấy thông tin của code ra để tính
            if (codeTypeId > 0)
            {
                var codeTypeFilter = Builders<CodesTypeModel>.Filter.Eq("CodeTypeId", codeTypeId);
                var codeType = await repository.CodesTypeRepository.Single(codeTypeFilter);
                if (codeType != null)
                {
                    prefix = codeType.CodeTypePrefix ?? "";
                }
            }

            var productCode = await repository.ProductRepository.SequenceIds(prefix);
            doc.CreateDate = createDate;
            doc.Creator = tokenView.Staff_id;
            doc.User_id = tokenView.User_id;
            doc.Location_id = tokenView.Location_id;
            var productName = doc.ProductName;
            doc.ProductCode = productCode._key.ToString();
            var productNameAcronymn = productName.ToAcronymn();
            var productNameAnsi = UnicodeToAnsiHelper.UnicodeToAnsi(productName);
            doc.ProductNameAcronymn = productNameAcronymn;
            doc.ProductNameAnsi = productNameAnsi;
            doc.Color ??= ColorHelper.Color();
            var productPost = await repository.ProductRepository.Post(doc);
            if (productPost != null)
            {
                if (productPost?._id.ToString() != "")
                {
                    var responseEntity = new ResponseEntityModel
                    {
                        _id = productPost._id.ToString()
                    };
                    response.Message = String.Format(localizer["response_post"].Value, productName);
                    response.Model = responseEntity;
                }
                else
                {
                    response.Message = String.Format(localizer["response_post"].Value, productName);
                    response.HttpStatusCode = 500;
                    return response;
                }
            }
            else
            {
                response.Message = String.Format(localizer["500"].Value, "");
                response.HttpStatusCode = 500;
                return response;
            }
            return response;
        }
        #endregion
        #region Put
        public async Task<Response> Put(string _id, ProductModel doc)
        {
            var response = new Response { HttpStatusCode = 200 };
            if (doc == null || doc.ProductName == null)
            {
                response.Message = localizer["doc"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            if (doc.ProductName == null || doc.ProductName == "")
            {
                response.Message = localizer["productName_blank"].Value;
                return response;
            }
            var createDate = ConvertHelper.Now();
            if (_id != null && _id != "")
            {
                var productRepository = await repository.ProductRepository.SingleById(_id);
                if (productRepository != null && productRepository._id != null && productRepository._id.ToString() != "")
                {

                    var filter = Builders<ProductModel>.Filter.Eq("_id", ObjectId.Parse(_id));
                    var productName = doc.ProductName;
                    var productNameAcronymn = doc.ProductNameAcronymn ?? productName.ToAcronymn();
                    var productNameAnsi = UnicodeToAnsiHelper.UnicodeToAnsi(productName, " ");
                    var quotaQuantity = doc.QuotaQuantity ?? 0;
                    var sellprice = doc.SellPrice ?? 0;
                    var comparePrice = doc.ComparePrice ?? 0;
                    var updateDefinition = Builders<ProductModel>.Update
                        .Set("ProductName", doc.ProductName)
                        .Set("ProductNameAcronymn", productNameAcronymn)
                        .Set("ProductNameAnsi", productNameAnsi)
                        .Set("QuotaQuantity", quotaQuantity)
                        .Set("RefId", doc.RefId)
                        .Set("SellPrice", sellprice)
                        .Set("ComparePrice", comparePrice)
                        .Set("Color", doc.Color)
                        .Set("Sku", doc.Sku)
                        .Set("Url", doc.Url)
                        .Set("Description", doc.Description)
                        .Set("Note", doc.Note)
                        .Set("UnitId", doc.UnitId)
                        .Set("ProductTypeId", doc.ProductTypeId)
                        .Set("IsPublish", doc.IsPublish)
                        .Set("UpdateDate", createDate);
                    var productPost = await repository.ProductRepository.Put(filter, updateDefinition);
                    if (productPost)
                    {
                        var responseEntity = new ResponseEntityModel
                        {
                            _id = _id
                        };
                        response.Message = String.Format(localizer["response_put"].Value, productName);
                        response.Model = responseEntity;
                    }
                    else
                    {
                        response.Message = String.Format(localizer["500"].Value, "");
                        response.HttpStatusCode = 500;
                        return response;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Bookmark put
        /// <summary>
        /// Product Bookmark Put
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public async Task<Response> BookmarkPut(string _id)
        {
            var response = new Response { HttpStatusCode = 200 };
            var createDate = ConvertHelper.Now();
            if (_id != null && _id != "")
            {
                var productRepository = await repository.ProductRepository.SingleById(_id);
                if (productRepository != null && productRepository._id != null && productRepository._id.ToString() != "")
                {
                    var productName = productRepository.ProductName;
                    var bookmark = productRepository.Bookmark.Equals(1) ? 0 : 1;
                    var filter = Builders<ProductModel>.Filter.Eq("_id", ObjectId.Parse(_id));
                    var updateDefinition = Builders<ProductModel>.Update
                        .Set("Bookmark", bookmark)
                          .Set("UpdateDate", createDate);
                    var productPost = await repository.ProductRepository.Put(filter, updateDefinition);
                    if (productPost)
                    {
                        var responseEntity = new ResponseEntityModel
                        {
                            _id = _id
                        };
                        response.Message = String.Format(localizer["response_put"].Value, productName);
                        response.Model = responseEntity;
                    }
                    else
                    {
                        response.Message = String.Format(localizer["500"].Value, "");
                        response.HttpStatusCode = 500;
                        return response;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Publish Put
        /// <summary>
        /// Publish Put
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public async Task<Response> PublishPut(string _id)
        {
            var response = new Response { HttpStatusCode = 200 };
            var createDate = ConvertHelper.Now();
            if (_id != null && _id != "")
            {
                var productRepository = await repository.ProductRepository.SingleById(_id);
                if (productRepository != null && productRepository._id != null && productRepository._id.ToString() != "")
                {
                    var productName = productRepository.ProductName;
                    var filter = Builders<ProductModel>.Filter.Eq("_id", ObjectId.Parse(_id));
                    var updateDefinition = Builders<ProductModel>.Update
                        .Set("IsPublish", true)
                          .Set("UpdateDate", createDate);
                    var productPost = await repository.ProductRepository.Put(filter, updateDefinition);
                    if (productPost)
                    {
                        var responseEntity = new ResponseEntityModel
                        {
                            _id = _id
                        };
                        response.Message = String.Format(localizer["response_put"].Value, productName);
                        response.Model = responseEntity;
                    }
                    else
                    {
                        response.Message = String.Format(localizer["500"].Value, "");
                        response.HttpStatusCode = 500;
                        return response;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Response> Delete(string _id)
        {
            var response = new Response { HttpStatusCode = 200 };
            var createDate = ConvertHelper.Now();
            if (_id != null && _id != "")
            {
                var productRepository = await repository.ProductRepository.SingleById(_id);
                if (productRepository != null && productRepository._id != null && productRepository._id.ToString() != "")
                {
                    var productName = productRepository.ProductName;
                    var productPost = await repository.ProductRepository.Delete(_id);
                    if (productPost)
                    {
                        var responseEntity = new ResponseEntityModel
                        {
                            _id = _id
                        };
                        response.Message = String.Format(localizer["response_delete"].Value, productName);
                        response.Model = responseEntity;
                    }
                    else
                    {
                        response.Message = String.Format(localizer["500"].Value, "");
                        response.HttpStatusCode = 500;
                        return response;
                    }
                }
            }
            return response;
        }
        #endregion
    }
}
