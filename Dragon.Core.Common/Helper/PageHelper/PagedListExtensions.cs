using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Common
{
    public static class PagedListExtensions
    {
        /// <summary>
        /// PagedList
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex">1为起始页</param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        public static async Task<PageModel<T>> ToPagedListAsync<T>(
            this IQueryable<T> query,
            int pageIndex=1,
            int pageSize=10,
            CancellationToken cancellationToken = default)
        {
            if (pageIndex < 1) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            int realIndex = pageIndex - 1;
            int count = await query.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await query.Skip(realIndex * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(cancellationToken)
                                   .ConfigureAwait(false);
            return new PageModel<T>(items, pageIndex, pageSize, count);
        }

        public static PageModel<T> ToPagedList<T>(
            this IQueryable<T> query,
            int pageIndex=1,
            int pageSize=10)
        {
            if (pageIndex < 1) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            int realIndex = pageIndex - 1;
            int count = query.Count();
            var items = query.Skip(realIndex * pageSize)
                             .Take(pageSize)
                             .ToList();
            return new PageModel<T>(items, pageIndex, pageSize, count);
        }
    }
}
