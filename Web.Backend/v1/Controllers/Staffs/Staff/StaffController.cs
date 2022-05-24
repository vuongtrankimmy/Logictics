using System.Threading.Tasks;
using BusinessLogicLayer.BusinessWrapper;
using Cores.Body;
using Entities.OwnerModels.Logictics.Staffs.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.v1.Base;

namespace Web.Backend.v1.Controllers.Staffs.Staff
{
    public class StaffController : BaseController
    {
        readonly IBusinessWrapper businessWrapper;
        public StaffController(IBusinessWrapper businessWrapper)
        {
            this.businessWrapper = businessWrapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Paging(string keyword="",int pageIndex=1,int pageSize=10)
        {
            var post = await businessWrapper.StaffBal.Paging(keyword,pageIndex,pageSize);
            if (post.HttpStatusCode.Equals(200))
            {
                return Ok(post);
            }
            return BadRequest(post);
        }
        [HttpGet("{_id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBy_id(string _id="")
        {
            var post = await businessWrapper.StaffBal.GetBy_id(_id);
            if (post.HttpStatusCode.Equals(200))
            {
                return Ok(post);
            }
            return BadRequest(post);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post()
        {
            var doc = await Request.GetRawBodyAsync<StaffModel>();
            var post = await businessWrapper.StaffBal.Post(doc);
            if (post.HttpStatusCode.Equals(200))
            {
                return Ok(post);
            }
            return BadRequest(post);
        }

        [HttpPost("export")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportPost()
        {
            var doc = await Request.GetRawBodyAsync<ExportFileModel>();
            var post = await businessWrapper.StaffBal.Export(doc);
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
            var doc = await Request.GetRawBodyAsync<StaffModel>();
            var post = await businessWrapper.StaffBal.Put(_id, doc);
            if (post.HttpStatusCode.Equals(200))
            {
                return Ok(post);
            }
            return BadRequest(post);
        }
    }
}
