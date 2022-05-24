using System.Threading.Tasks;
using BusinessLogicLayer.BusinessWrapper;
using Cores.Body;
using Entities.OwnerModels.Logictics.Products.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.v1.Base;

namespace Web.Backend.v1.Controllers.Products.Product
{
    public class ProductController : BaseController
    {
        readonly IBusinessWrapper businessWrapper;
        public ProductController(IBusinessWrapper businessWrapper)
        {
            this.businessWrapper = businessWrapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Paging(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            var get = await businessWrapper.ProductBal.Paging(keyword, pageIndex, pageSize);
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
            var get = await businessWrapper.ProductBal.GetBy_id(_id);
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
            var doc = await Request.GetRawBodyAsync<ProductModel>();
            var post = await businessWrapper.ProductBal.Post(doc);
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
            var doc = await Request.GetRawBodyAsync<ProductModel>();
            var put = await businessWrapper.ProductBal.Put(_id, doc);
            if (put.HttpStatusCode.Equals(200))
            {
                return Ok(put);
            }
            return BadRequest(put);
        }

        [HttpPut("bookmark/{_id}")]
        [AllowAnonymous]
        public async Task<IActionResult> BookmarkPut(string _id)
        {
            var post = await businessWrapper.ProductBal.BookmarkPut(_id);
            if (post.HttpStatusCode.Equals(200))
            {
                return Ok(post);
            }
            return BadRequest(post);
        }

        [HttpPut("publish/{_id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PublishPut(string _id)
        {
            var put = await businessWrapper.ProductBal.PublishPut(_id);
            if (put.HttpStatusCode.Equals(200))
            {
                return Ok(put);
            }
            return BadRequest(put);
        }

        [HttpDelete("{_id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(string _id)
        {
            var delete = await businessWrapper.ProductBal.Delete(_id);
            if (delete.HttpStatusCode.Equals(200))
            {
                return Ok(delete);
            }
            return BadRequest(delete);
        }
    }
}
