using System.Threading.Tasks;
using BusinessLogicLayer.BusinessWrapper;
using Cores.Body;
using Entities.OwnerModels.Functions.Staffs.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.v1.Base;

namespace Web.Backend.v1.Controllers.Staffs.Authentication
{
    public class StaffsAuthenticationController : BaseController
    {
        readonly IBusinessWrapper businessWrapper;
        public StaffsAuthenticationController(IBusinessWrapper businessWrapper)
        {
            this.businessWrapper = businessWrapper;
        }       
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post()
        {
            var doc = await Request.GetRawBodyAsync<StaffFuncModel>(true);
            var post = await businessWrapper.SigninAuthenticationBAL.Signin(doc);
            if (post.HttpStatusCode.Equals(200))
            {
                return Ok(post);
            }
            return BadRequest(post);
        }
    }
}
