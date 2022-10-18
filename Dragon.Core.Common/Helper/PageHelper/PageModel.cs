using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Common
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class PageModel<T>
    {
        public PageModel()
        {

        }
        public PageModel(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            page = pageIndex;
            PageSize = pageSize;
            Total = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items;
            HasNextPage = pageIndex < TotalPages;
            HasPrevPage = pageIndex - 1 > 0;

        }
        /// <summary>
        /// 当前页标
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; }
        /// <summary>
        /// 返回数据
        /// </summary>
        /// <summary>
        /// 当前页集合
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrevPage { get; set; }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage { get; set; }

    }

    /// <summary>
    /// 全局分页查询输入参数
    /// </summary>
    public class BasePageInput
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public virtual int Page { get; set; }

        /// <summary>
        /// 页码容量
        /// </summary>
        public virtual int PageSize { get; set; } = 10;
    }
}
