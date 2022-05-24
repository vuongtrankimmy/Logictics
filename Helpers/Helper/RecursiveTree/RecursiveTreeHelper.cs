using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.OwnerModels.Logictics.Systems.Navigations.WebPage;

namespace Helpers.Helper.RecursiveTree
{
    public static class RecursiveTreeHelper
    {
        public static async Task<List<NavigationWebPageTreeModel>> BindTree(List<NavigationWebPageModel> navigationWebPageList, NavigationWebPageTreeModel parentNode)
        {
            var treeList = new List<NavigationWebPageTreeModel>();
            var list = navigationWebPageList.Where(x => parentNode == null ? x.ParentPageId == 0 : x.ParentPageId == parentNode.PageId);
            foreach (var item in list)
            {
                var hasSub = navigationWebPageList.Where(x => x.ParentPageId.Equals(item.PageId)).Any() ? "has-sub" : "";
                var toggle = hasSub != "" ? "nk-menu-toggle" : "";
                var tree = new NavigationWebPageTreeModel
                {
                    _id = item._id,
                    _key = item._key,
                    _rev = item._rev,
                    PageId = item.PageId,
                    Name = item.Name,
                    Handle = item.Handle,
                    RelativeUrl = item.RelativeUrl,
                    Enable = item.Enable,
                    ParentPageId = item.ParentPageId ?? 0,
                    HasSub = hasSub,
                    Toggle = toggle,
                    Icon = item.Icon
                };
                switch (parentNode)
                {
                    case null:
                        treeList.Add(tree);
                        break;
                    default:
                        parentNode.Sub.Add(tree);
                        break;
                }
                //if (parentNode != null)
                //    parentNode.HasSub = hasSub;
                await BindTree(navigationWebPageList, tree);
            }
            return treeList;
        }
    }
}
