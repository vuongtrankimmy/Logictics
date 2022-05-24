using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BusinessLogicLayer.Owners.Logictics.Products.ProductAllocationRequired;
using Contracts.RepositoryWrapper;
using Entities.Enumerations.Fields;
using Entities.ExtendedModels.Localize;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.Extensions.NullDySession;
using Entities.Extensions.Response;
using Entities.Extensions.Tokens;
using Entities.OwnerModels.Logictics.Products.Product;
using Entities.OwnerModels.Logictics.Products.ProductAllocationRequired;
using Entities.OwnerModels.Logictics.Staffs.Staff;
using Entities.OwnerModels.Logictics.Systems.CodesTypes.CodesType;
using Entities.OwnerModels.Logictics.Systems.Products.ProductType;
using Entities.OwnerModels.Logictics.Systems.Units.Unit;
using Helpers.Helper.Convert;
using Helpers.Helper.Paging;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLayer.Owners.Logictics.Products.ProductAllocationRequired
{
    public class ProductAllocationRequiredDal : IProductAllocationRequiredBal
    {
        readonly TokenView tokenView = NullDySession.TokenUser ?? new TokenView();
        readonly IRepositoryWrapper repository;
        private readonly IStringLocalizer<Resource> localizer;
        public int productAllocationRequiredField = (int)Fields.ProductAllocationRequired;
        public int productTypeId = (int)ProductType.Product;
        public ProductAllocationRequiredDal(IStringLocalizer<Resource> localizer, IRepositoryWrapper repository)
        {
            this.repository = repository;
            this.localizer = localizer;
        }

        #region Paging
        /// <summary>
        /// Phân trang danh sách khách hàng
        /// 2022/04/23
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
            var filter = Builders<ProductAllocationRequiredViewModel>.Filter.Regex("InvoiceNumber", keyword)
                | Builders<ProductAllocationRequiredViewModel>.Filter.Regex("ProductAllocationRequiredCode", keyword);
            var sort = Builders<ProductAllocationRequiredViewModel>.Sort.Descending("_id").Descending("Create");
            var productAllocationRequiredList = await repository.ProductAllocationRequiredRepository.Page(filter, sort, pageIndex, pageSize);
            var httpCodeStatus = 404;
            if (productAllocationRequiredList != null && productAllocationRequiredList.ItemTotal > 0)
            {
                var productsAllocationRequired = productAllocationRequiredList.List;
                #region Creator Id List
                var creatorIdList = productsAllocationRequired.Where(x => x.Creator != null && x.Creator != "").Select(x => ObjectId.Parse(x.Creator)).Distinct();
                var creatorList = new List<StaffModel>();
                if (creatorIdList != null && creatorIdList.Any())
                {
                    creatorList = await repository.StaffRepository.GetInListByType("_id", creatorIdList);
                }
                #endregion                

                #region Product List
                var product_idList = productsAllocationRequired.Where(x => x.Product_id != null && x.Product_id != "").Select(x => ObjectId.Parse(x.Product_id)).Distinct();
                var productList = new List<ProductViewModel>();
                if (product_idList != null && product_idList.Any())
                {
                    productList = await repository.ProductRepository.GetInListByType("_id", product_idList);
                }
                #endregion

                var productListFromPAR = (from productAR in productsAllocationRequired
                                          from productA in productAR.ProductList
                                          let product_id = productA.Product_id
                                          where product_id != null && product_id != ""
                                          select ObjectId.Parse(product_id)).Distinct().ToList();
                var productInList = new List<ProductViewModel>();
                if (productListFromPAR.Count > 0)
                {
                    var product_idPARJson = ConvertHelper.Serialize(productListFromPAR);
                    productInList = await repository.ProductRepository.GetInList("_id", product_idPARJson);
                }

                var unitList = new List<UnitModel>();
                var codesTypeList = new List<CodesTypeModel>();
                var productTypeList = new List<ProductTypeModel>();
                if (productList != null && productList.Any())
                {

                    #region Unit List                             
                    var unitIdList = productList.Where(x => x.UnitId != null && x.UnitId > 0).Select(x => x.UnitId ?? 0).Distinct();
                    if (unitIdList != null && unitIdList.Any())
                    {
                        unitList = await repository.UnitRepository.GetInListById("UnitId", unitIdList);
                    }
                    #endregion

                    #region CodesType List
                    var codesTypeIdList = productList.Where(x => x.CodeTypeId != null && x.CodeTypeId > 0).Select(x => x.CodeTypeId ?? 0).Distinct();
                    if (codesTypeIdList != null && codesTypeIdList.Any())
                    {
                        codesTypeList = await repository.CodesTypeRepository.GetInListById("CodeTypeId", codesTypeIdList);
                    }
                    #endregion
                }

                foreach (var productAllocationRequired in productsAllocationRequired)
                {
                    var product_id = productAllocationRequired.Product_id;
                    string productAllocationRequiredCode = productAllocationRequired.ProductAllocationRequiredCode;
                    var codeTypeId = productAllocationRequired.CodeTypeId ?? 0;
                    var product = productList.Where(x => x._id.Equals(product_id)).FirstOrDefault();
                    if (product != null)
                    {
                        var creator_id = product.Creator ?? "";
                        var productTypeId = product.ProductTypeId ?? 0;
                        var unitId = product.UnitId ?? 0;
                        productAllocationRequired.ProductName = product.ProductName;
                        productAllocationRequired.ProductCode = product.ProductCode;
                        productAllocationRequired.Url = product.Url;
                        var materialTotal = productAllocationRequired.ProductList.Count();
                        productAllocationRequired.MaterialTotal = materialTotal <= 10 ? materialTotal.ToString() : materialTotal.ToString() + "+";
                        if (creator_id != "")
                        {
                            if (creatorList != null && creatorList.Any())
                            {
                                var creator = creatorList.Where(x => x._id.Equals(creator_id)).FirstOrDefault();
                                if (creator != null)
                                {
                                    productAllocationRequired.CreatorName = creator.Nickname ?? creator.StaffName;
                                    productAllocationRequired.CreatorAvatar = creator.Avatar;
                                    productAllocationRequired.CreatorAcronymn = creator.StaffNameAcronymn;
                                }
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
                                var productCode = productAllocationRequiredCode.ToCode(numberFormat, prefix, hyphen);
                                productAllocationRequired.ProductAllocationRequiredCode = productCode;
                            }
                        }

                        var productARList = productAllocationRequired.ProductList;
                        if (productARList != null && productARList.Any())
                        {
                            foreach (var productAR in productARList)
                            {
                                var productAR_id = productAR.Product_id;
                                if (productInList != null && productInList.Any())
                                {
                                    var productIn = productInList.Where(x => x._id.Equals(productAR_id)).FirstOrDefault();
                                    if (productIn != null)
                                    {
                                        productAR.ProductName = productIn.ProductName;
                                        productAR.ProductCode = productIn.ProductCode;
                                    }
                                }
                            }

                            productAllocationRequired.MaterialTop = String.Join(", ", productARList.Select(x => x.ProductCode).Take(3));
                        }
                    }
                }

                httpCodeStatus = 200;
                var page = PagingHelper.Get(keyword, pageIndex, pageSize, decimal.Parse(productAllocationRequiredList.ItemTotal.ToString()));
                productAllocationRequiredList.Pages = page;
                productAllocationRequiredList.Keyword = keyword;
            }
            var response = new Response
            {
                HttpStatusCode = httpCodeStatus,
                Model = productAllocationRequiredList
            };
            return response;
        }
        #endregion

        #region Get by _id
        /// <summary>
        /// Lấy thông tin chi tiết khách hàng theo _id
        /// 2022/05/24
        /// Trần Vương
        /// </summary>
        /// <param name="_id">Mã khách hàng theo ObjectId</param>
        /// <returns></returns>
        public async Task<Response> GetBy_id(string _id)
        {
            var httpCodeStatus = 404;
            var productAllocationRequired = new ProductAllocationRequiredFormModel();
            var codeTypeId = 0;
            var allocationProduct_id = "";
            _id ??= "";
            if (_id != null && _id != "")
            {
                var productAllocationRequiredRepository = await repository.ProductAllocationRequiredRepository.SingleFormById(_id);
                if (productAllocationRequiredRepository?._id != null && productAllocationRequiredRepository._id.ToString() != "")
                {
                    productAllocationRequired = productAllocationRequiredRepository;
                    codeTypeId = productAllocationRequiredRepository.CodeTypeId ?? 0;
                    allocationProduct_id = productAllocationRequiredRepository.Product_id ?? "";
                    httpCodeStatus = 200;
                }
            }
            else
            {
                httpCodeStatus = 200;
            }
            var select = "selected";
            var check = "checked";
            #region Product Choose List 
            var productFilter = Builders<ProductChooseModel>.Filter.Eq("ProductTypeId", productTypeId);
            var productSort = Builders<ProductChooseModel>.Sort.Ascending("ProductCode");
            var productChooseList = await repository.ProductRepository.ListChooseByLimit(productFilter, productSort);
            if (productChooseList != null && productChooseList.Any())
            {
                if (allocationProduct_id != "")
                {
                    var allocationRequiredProduct = productChooseList.Where(x => x._id.Equals(allocationProduct_id)).FirstOrDefault();
                    if (allocationRequiredProduct != null)
                    {
                        productChooseList.ForEach(x => { x.Default = false; x.select = ""; });
                        allocationRequiredProduct.Default = true;
                        allocationRequiredProduct.select = select;
                        productAllocationRequired.Product = allocationRequiredProduct;
                    }
                }
                else
                {
                    var allocationRequiredProduct = productChooseList.FirstOrDefault();
                    if (allocationRequiredProduct != null)
                    {
                        productChooseList.ForEach(x => { x.Default = false; x.select = ""; });
                        allocationRequiredProduct.Default = true;
                        allocationRequiredProduct.select = select;
                    }
                }
            }
            productAllocationRequired.ProductChooseList = productChooseList;
            #endregion
            #region Code Type List
            var codesTypeFilter = Builders<CodesTypeModel>.Filter.Eq("CodeTypeGroupId", productAllocationRequiredField);//2: Product
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
                if (productAllocationRequired?.ProductCode == null || productAllocationRequired?.ProductCode == "")
                {
                    long _key = 1;
                    var sequenIds = await repository.ProductRepository.SequenceIdsGet(prefix);//Chỗ này xử lý đúng thì xem xét lấy id cuối cùng của danh sách.
                    if (sequenIds != null && sequenIds._key > 0)
                    {
                        _key = sequenIds._key;
                    }
                    productCode = _key.ToString().ToCode(numberFormat, prefix, hyphen);
                }
                productAllocationRequired.ProductCode = productCode;
            }
            productAllocationRequired.CodesTypeName = productCodeTypeName;
            productAllocationRequired.CodeTypeList = codesTypeList;
            #endregion

            //AllocationRequired=AR
            //ProductAllocationRequired=PAR
            var productARList = productAllocationRequired.ProductList;
            if (productARList != null && productARList.Any())
            {
                var productListFromPAR = (from productA in productARList
                                          let product_id = productA.Product_id
                                          where product_id != null && product_id != ""
                                          select ObjectId.Parse(product_id)).Distinct().ToList();
                if (productListFromPAR.Count > 0)
                {
                    var product_idPARJson = ConvertHelper.Serialize(productListFromPAR);
                    var productInList = await repository.ProductRepository.GetInList("_id", product_idPARJson);
                    foreach (var productAR in productARList)
                    {
                        var productAR_id = productAR.Product_id;
                        if (productInList != null && productInList.Any())
                        {
                            var productIn = productInList.Where(x => x._id.Equals(productAR_id)).FirstOrDefault();
                            if (productIn != null)
                            {
                                productAR.ProductName = productIn.ProductName;
                                productAR.ProductCode = productIn.ProductCode;
                            }
                        }
                    }
                }
            }

            var response = new Response
            {
                HttpStatusCode = httpCodeStatus,
                Model = productAllocationRequired
            };
            return response;
        }
        #endregion

        #region Post
        /// <summary>
        /// Post
        /// 2022/05/24
        /// Trần Vương
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public async Task<Response> Post(ProductAllocationRequiredModel doc)
        {
            var response = new Response { HttpStatusCode = 200 };
            if (doc == null || doc.Product_id == null)
            {
                response.Message = localizer["doc"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            if (doc.Product_id == null || doc.Product_id == "")
            {
                response.Message = localizer["ProductName_blank"].Value;
                response.HttpStatusCode = 204;
                return response;
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

            var createDate = ConvertHelper.Now();
            var productCode = await repository.ProductRepository.SequenceIds(prefix);
            doc.CreateDate = createDate;
            doc.Creator = tokenView.Staff_id;
            doc.User_id = tokenView.User_id;
            doc.Location_id = tokenView.Location_id;
            doc.ProductAllocationRequiredCode = productCode._key.ToString();
            var allocationRequiredPost = await repository.ProductAllocationRequiredRepository.Post(doc);
            if(allocationRequiredPost != null)
            {
                if (allocationRequiredPost?._id.ToString() != "")
                {
                    var responseEntity = new ResponseEntityModel
                    {
                        _id = allocationRequiredPost._id.ToString()
                    };
                    response.Message = String.Format(localizer["response_post"].Value, allocationRequiredPost._id);
                    response.Model = responseEntity;
                }
                else
                {
                    response.Message = String.Format(localizer["response_post"].Value, allocationRequiredPost._id);
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
        /// <summary>
        /// Put
        /// 2022/05/24
        /// Trần Vương
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public async Task<Response> Put(string _id, ProductAllocationRequiredModel doc)
        {
            var response = new Response { HttpStatusCode = 200 };
            if (doc == null || doc.Product_id == null)
            {
                response.Message = localizer["doc"].Value;
                response.HttpStatusCode = 204;
                return response;
            }
            if (doc.Product_id == null || doc.Product_id == "")
            {
                response.Message = localizer["ProductName_blank"].Value;
                response.HttpStatusCode = 204;
                return response;
            }

            var createDate = ConvertHelper.Now();
            if (_id != null && _id != "")
            {
                var productAllocationRequiredRepository = await repository.ProductAllocationRequiredRepository.SingleById(_id);
                if (productAllocationRequiredRepository != null && productAllocationRequiredRepository._id != null && productAllocationRequiredRepository._id.ToString() != "")
                {

                    var filter = Builders<ProductAllocationRequiredModel>.Filter.Eq("_id", ObjectId.Parse(_id));
                    var updateDefinition = Builders<ProductAllocationRequiredModel>.Update
                        .Set("Product_id", doc.Product_id)
                        .Set("InvoiceNumber", doc.InvoiceNumber)
                        .Set("IsPublish", doc.IsPublish)
                        .Set("Quantity", doc.Quantity)
                        .Set("Loss", doc.Loss)
                        .Set("SourceTypeId", doc.SourceChooseTypeId)
                        .Set("ProductList",doc.ProductList)
                        .Set("UpdateDate", createDate);
                    var productPost = await repository.ProductAllocationRequiredRepository.Put(filter, updateDefinition);
                    if (productPost)
                    {
                        var responseEntity = new ResponseEntityModel
                        {
                            _id = _id
                        };
                        response.Message = String.Format(localizer["response_put"].Value, _id);
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
