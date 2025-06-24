using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereStringContains<T>(this IQueryable<T> source, string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(value))
                return source;

            var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (property == null)
                throw new ArgumentException($"La propriété '{propertyName}' n'existe pas sur {typeof(T).Name}");

            if (property.PropertyType != typeof(string))
                throw new ArgumentException($"'{propertyName}' n'est pas une propriété de type 'string' sur {typeof(T).Name}");

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, property);
            var toLower = Expression.Call(propertyAccess, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
            var contains = Expression.Call(toLower, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(value.ToLower()));

            var lambda = Expression.Lambda<Func<T, bool>>(contains, parameter);

            return source.Where(lambda);
        }

    }
}
