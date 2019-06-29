using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WeatherForecast.CSharp.API.Infrastructure
{
    public static class Extensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> source, IEnumerable<string> navigationPropertyPaths)
            where T : class
        {
            return navigationPropertyPaths.Aggregate(source, (query, path) => query.Include(path));
        }

        public static IEnumerable<string> GetIncludePaths<T>(this DbContext context)
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var includedProperties = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();
            while (true)
            {
                var entityProperties = new List<INavigation>();
                foreach (var navigation in entityType.GetNavigations())
                {
                    if (includedProperties.Add(navigation))
                        entityProperties.Add(navigation);
                }

                if (entityProperties.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
                }
                else
                {
                    foreach (var navigation in entityProperties)
                    {
                        var inverseNavigation = navigation.FindInverse();
                        if (inverseNavigation != null)
                            includedProperties.Add(inverseNavigation);
                    }

                    stack.Push(entityProperties.GetEnumerator());
                }

                while (stack.Count > 0 && !stack.Peek().MoveNext())
                {
                    var enumerator = stack.Pop();
                    enumerator.Dispose();
                }
                if (stack.Count == 0) break;
                entityType = stack.Peek().Current.GetTargetType();
            }
        }
    }
}