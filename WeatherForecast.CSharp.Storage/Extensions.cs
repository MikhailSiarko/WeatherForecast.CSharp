using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.CSharp.Storage
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
                        var inverseNavigation = navigation.Inverse;
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
                entityType = (stack.Peek().Current ?? throw new InvalidOperationException()).TargetEntityType;
            }
        }

        public static IServiceCollection ConfigureStorage(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
        {
            return services.AddDbContextPool<AppDbContext>(builder =>
            {
                builder.UseSqlite(configuration.GetConnectionString(connectionStringName));
            });
        }

        public static IServiceCollection RegisterAutomapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}