using System.Threading.Tasks;
using BusinessLogicLayer.BusinessWrapper;
using Entities.OwnerModels.Logictics.Customers.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.v1.Base;

namespace Web.Backend.v1.Controllers.Customers.Customer
{
    public class CustomerController : BaseController
    {
        readonly IBusinessWrapper businessWrapper;
        public CustomerController(IBusinessWrapper businessWrapper)
        {
            this.businessWrapper = businessWrapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int pageIndex=1, int pageSize=10)
        {
            var get = await businessWrapper.CustomerBal.Get(pageIndex,pageSize);
            return Ok(get);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CustomerModel customerModel)
        {
            var post = await businessWrapper.CustomerBal.Post(customerModel);
            return Ok(post);
        }
    }
}