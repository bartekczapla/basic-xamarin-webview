using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BasicXamarin.Shared
{
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, OrderElementDescription descriptor)
        {
            var type = typeof(T);
            var property = type.GetProperty(descriptor.Property);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var typeArguments = new Type[] { type, property.PropertyType };
            var methodName = descriptor.IsAscending ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression, Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static void AddCollection<T>(this ObservableCollection<T> collection, IEnumerable<T> newCollection)
        {
            foreach(var item in newCollection)
            {
                collection.Add(item);
            }
        }
    }
}
