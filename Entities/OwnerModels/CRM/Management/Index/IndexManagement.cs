using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.OwnerModels.CRM.Management.Index
{
    public class IndexManagement
    {
        public string _id { get; set; } = "entity_id";
        public long? Value { get; set; } = 1;
    }

    public class IndexManagementModel
    {
        public string _id { get; set; }
        public long _key { get; set; }
        public string _rev { get; set; }
    }
}
