using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Yourslides.Model;
using Yourslides.Model.SelectionOptions;

namespace Yourslides.Data.Infrastructure {
    public static class EFExtensions {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, PropertyInfo sortField, SortType sort) {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            string method = sort == SortType.Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}