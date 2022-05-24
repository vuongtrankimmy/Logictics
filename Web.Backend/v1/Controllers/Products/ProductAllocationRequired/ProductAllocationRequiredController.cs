using System.Threading.Tasks;
using BusinessLogicLayer.BusinessWrapper;
using Cores.Body;
using Entities.OwnerModels.Logictics.Products.Product;
using Entities.OwnerModels.Logictics.Products.ProductAllocationRequired;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.v1.Base;

namespace Web.Backend.v1.Controllers.Products.ProductAllocationRequired
{    
    public class ProductAllocationRequiredController : BaseController
    {
        readonly IBusinessWrapper businessWrapper;
        public ProductAllocationRequiredController(IBusinessWrapper businessWrapper)
        {
            this.businessWrapper = businessWrapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Paging(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            var get = await businessWrapper.ProductAllocationRequiredBal.Paging(keyword, pageIndex, pageSize);
            if (get.HttpStatusCode.Equals(200))
            {
                return Ok(get);
            }
            return BadRequest(get);
        }


        [HttpGet("{_id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBy_id(string _id = "")
        {
            var get = await businessWrapper.ProductAllocationRequiredBal.GetBy_id(_id);
            if (get.HttpStatusCode.Equals(200))
            {
                return Ok(get);
            }
            return BadRequest(get);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post()
        {
            var doc = await Request.GetRawBodyAsync<ProductAllocationRequiredModel>();
            var post = await businessWrapper.ProductAllocationRequiredBal.Post(doc);
            if (post.HttpStatusCode.Equals(200))
            {
                return Ok(post);
            }
            return BadRequest(post);
        }

        [HttpPut("{_id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Put(string _id)
        {
            var doc = await Request.GetRawBodyAsync<ProductAllocationRequiredModel>();
            var put = await businessWrapper.ProductAllocationRequiredBal.Put(_id, doc);
            if (put.HttpStatusCode.Equals(200))
            {
                return Ok(put);
            }
            return BadRequest(put);
        }
    }
}
