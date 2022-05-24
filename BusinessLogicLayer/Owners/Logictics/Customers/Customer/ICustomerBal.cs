using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.OwnerModels.Logictics.Customers.Customer;

namespace BusinessLogicLayer.Owners.Logictics.Customers.Customer
{
    public interface ICustomerBal
    {
        Task<Response> Get(int pageIndex=1, int pageSize=10);
        Task<Response> Post(CustomerModel doc);
    }
}
