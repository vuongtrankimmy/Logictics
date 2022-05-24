using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.BusinessWrapper;
using Microsoft.AspNetCore.Authorization;
using Web.Backend.v1.Base;

namespace Web.Backend.v1.Controllers.Navigations.WebHandler
{
    public class NavigationWebHandlerController : BaseController
    {
        readonly IBusinessWrapper businessWrapper;

        public NavigationWebHandlerController(IBusinessWrapper businessWrapper)
        {
            this.businessWrapper = businessWrapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var get = await businessWrapper.NavigationWebHandlerBAL.Get();
            return Ok(get);
        }
    }
}
