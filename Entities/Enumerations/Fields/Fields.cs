using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enumerations.Fields
{
    public enum Fields
    {
        Customer = 1,
        Product = 2,
        ProductAllocationRequired = 3,
        Staff = 4,
        InStock = 5,
        OutStock = 6,
        ProductGroup = 7
    }

    public enum ProductType
    {
        Product=1,
        Material=2
    }
}
