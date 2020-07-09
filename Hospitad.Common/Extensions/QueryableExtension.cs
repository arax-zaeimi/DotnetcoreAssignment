using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitad.Common.Extensions
{
    public static class QueryableExtension
    {
        public static async Task<IList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
