using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DTHManagementSystem.Infrastructure
{
    public static class IncludeHelper
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }

        //public static IQueryable<T> Select<T>(this IQueryable<T> query, params Expression<Func<T, object>> selects)
        //{
        //    if (selects != null)
        //    {
        //        query = query.Select(selects);

        //    }
        //    return query;
        //}
    }
}
