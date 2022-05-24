using System.Threading.Tasks;
using BusinessLogicLayer.BusinessWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.v1.Base;

namespace Web.Backend.v1.Controllers.Staffs.StaffGroup
{
    public class StaffGroupController : BaseController
    {
        readonly IBusinessWrapper businessWrapper;
        public StaffGroupController(IBusinessWrapper businessWrapper)
        {
            this.businessWrapper = businessWrapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var post = await businessWrapper.StaffGroupBal.GetList();
            if (post.HttpStatusCode.Equals(200))
            {
                return Ok(post);
            }
            return BadRequest(post);
        }
    }
}
