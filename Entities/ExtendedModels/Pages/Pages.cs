using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExtendedModels.Pages
{
    public partial class Pages : PagingBase
    {        
        public int TotalItemIndex { get; set; }
        public PagingItem Start { get; set; }
        public PagingItem End { get; set; }
        public PagingItem Preview { get; set; }
        public PagingItem Next { get; set; }
        public List<PagingItem> PagingItemList { get; set; }
        public bool Visible { get; set; } = false;
    }

    public class PagingItem: PagingBase
    {
        public int PagingNo { get; set; }
        public string PagingName { get; set; }
        public bool Active { get; set; } = false;
        public bool Enable { get; set; } = true;
        public string ActiveCss { get; set; } ="";
        public string DisabledCss { get; set; } = "";
    }
    public class PagingBase
    {
        public string Keyword { get; set; }
    }
}
