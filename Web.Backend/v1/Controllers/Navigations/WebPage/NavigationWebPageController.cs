using System.Threading.Tasks;
using BusinessLogicLayer.BusinessWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.v1.Base;

namespace Web.Backend.v1.Controllers.Navigations.WebPage
{
    public class NavigationWebPageController : BaseController
    {
        readonly IBusinessWrapper businessWrapper;

        public NavigationWebPageController(IBusinessWrapper businessWrapper)
        {
            this.businessWrapper = businessWrapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var get = await businessWrapper.NavigationWebPageBAL.Get();
            return Ok(get);
        }
        [HttpGet("route")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoute()
        {
            var get = await businessWrapper.NavigationWebPageBAL.WebsitePageRoute();
            return Ok(get);
        }
    }
}
