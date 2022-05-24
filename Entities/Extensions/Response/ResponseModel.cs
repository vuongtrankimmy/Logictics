using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.Pages;

namespace Entities.Extensions.Response
{
    public class ResponseModel
    {
        public int? HttpStatusCode { get; set; }
        public string Message { get; set; }
        public object Model { get; set; }
    }

    public partial class TEntityPaging<TEntity>
    {
        public string Keyword { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public decimal? PageTotal { get; set; }
        public long? ItemTotal { get; set; }
        public int? ReturnItemTotal { get; set; }
        public List<TEntity> List { get; set; }
        public Pages Pages { get; set; }
    }
    public partial class TEntityList<TEntity>
    {
        public List<TEntity> List { get; set; }
    }

    public class ResponseEntityModel
    {
        public string _id { get; set; }
    }
}
