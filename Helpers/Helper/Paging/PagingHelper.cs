using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ExtendedModels.Pages;

namespace Helpers.Helper.Paging
{
    public class PagingHelper
    {
        public static Pages Get(string keyword = "", int pageIndex = 1, int pageSize = 10, decimal totalItem = 1)
        {
            var totalPage = int.Parse(Math.Ceiling(totalItem / pageSize).ToString());
            var pageNumberMax = 4;
            var iStart = pageIndex - pageNumberMax < 1 ? 1 : pageIndex - pageNumberMax;
            var iEnd = iStart + (pageNumberMax + 1) > totalPage ? totalPage : iStart + (pageNumberMax + 1);

            var pagingItemList = new List<PagingItem>();

            var activeStart = pageIndex > 1;
            var enableStart = pageIndex > 1;
            var disableCssStart = enableStart ? "" : "disabled";
            var pagingStart = new PagingItem
            {
                PagingNo = 1,
                PagingName = "Start",
                Active = activeStart,
                Enable = enableStart,
                DisabledCss = disableCssStart,
                Keyword = keyword
            };


            var activeEnd = pageIndex < totalPage;
            var enableEnd = pageIndex < totalPage;
            var disableCssEnd = enableEnd ? "" : "disabled";
            var pagingend = new PagingItem
            {
                PagingNo = totalPage,
                PagingName = "End",
                Active = activeEnd,
                Enable = enableEnd,
                DisabledCss = disableCssEnd,
                Keyword = keyword
            };

            int pagingNoPreview = pageIndex <= 1 ? 1 : pageIndex - 1;
            var activePreview = pageIndex > 1;
            var enablePreview = pageIndex > 1;
            var disableCssPreview = enablePreview ? "" : "disabled";
            var pagingPreview = new PagingItem
            {
                PagingNo = pagingNoPreview,
                PagingName = "Preview",
                Active = activePreview,
                Enable = enablePreview,
                DisabledCss = disableCssPreview,
                Keyword = keyword
            };

            var pagingNoNext = pageIndex >= totalPage ? totalPage : pageIndex + 1;
            var activeNext = pageIndex < totalPage;
            bool enableNext = pageIndex < totalPage;
            var disableCssNext = enableNext ? "" : "disabled";
            var pagingNext = new PagingItem
            {
                PagingNo = pagingNoNext,
                PagingName = "Next",
                Active = activeNext,
                Enable = enableNext,
                DisabledCss = disableCssNext,
                Keyword = keyword
            };

            for (int i = iStart; i <= iEnd; i++)
            {
                var active = i == pageIndex;
                var cssActive = active ? "active" : "";
                var disableCss = active ? "disabled" : "";
                var paingItem = new PagingItem
                {
                    PagingNo = i,
                    PagingName = i.ToString(),
                    Active = active,
                    ActiveCss = cssActive,
                    DisabledCss = disableCss,
                    Keyword = keyword
                };
                pagingItemList.Add(paingItem);
            }
            var visible = pagingItemList.Count > 1;
            var totalItemIndex = pageIndex < totalPage ? pageIndex * pageSize : ((pageIndex - 1) * pageSize) + (int.Parse(totalItem.ToString()) - ((pageIndex - 1) * pageSize));
            if (visible)
            {
                var paging = new Pages
                {
                    Start = pagingStart,
                    End = pagingend,
                    Preview = pagingPreview,
                    Next = pagingNext,
                    Keyword = keyword,
                    TotalItemIndex = totalItemIndex,
                    PagingItemList = pagingItemList,
                    Visible = visible
                };
                return paging;
            }
            return default;
        }
    }

}
