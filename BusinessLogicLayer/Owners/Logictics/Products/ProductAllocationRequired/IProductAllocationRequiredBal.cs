using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.MethodErrorHandling;
using Entities.OwnerModels.Logictics.Products.ProductAllocationRequired;

namespace BusinessLogicLayer.Owners.Logictics.Products.ProductAllocationRequired
{
    public interface IProductAllocationRequiredBal
    {
        Task<Response> Paging(string keyword = "", int pageIndex = 1, int pageSize = 10);
        Task<Response> GetBy_id(string _id);
        Task<Response> Post(ProductAllocationRequiredModel doc);
        Task<Response> Put(string _id, ProductAllocationRequiredModel doc);
    }
}
