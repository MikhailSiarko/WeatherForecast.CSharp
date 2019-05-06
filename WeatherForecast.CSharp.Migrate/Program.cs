using System;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandLine;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.CSharp.Migrate
{
    internal enum MigrationType
    {
        Up,
        Down
    }

    internal class Options
    {
        [Option('t', "type", Required = true)] 
        public MigrationType Type { get; set; }
        
        [Option('v', "version", Required = false)]
        public int? Version { get; set; }
    }

    public class Program
    {
        private static IServiceCollection ConfigureServices()
        {
            return new ServiceCollection().AddFluentMigratorCore().ConfigureRunner(builder =>
            {
                builder.AddSQLite()
                    .WithGlobalConnectionString("Data Source=..\\Database.db")
                    .ScanIn(Assembly.GetExecutingAssembly())
                    .For.Migrations();
            });
        }

        private static void MigrateUp(int? version, IMigrationRunner runner)
        {
            if (version.HasValue && runner.HasMigrationsToApplyUp(version.Value))
            {
                runner.MigrateUp(version.Value);
            }
            else if(runner.HasMigrationsToApplyUp())
            {
                runner.MigrateUp();
            }
        }

        private static void MigrateDown(int? version, IMigrationRunner runner)
        {
            if (version.HasValue && runner.HasMigrationsToApplyDown(version.Value))
            {
                runner.MigrateDown(version.Value);
            }
        }

        private static void Run(Action<IMigrationRunner> action)
        {
            var services = ConfigureServices();
            using (var scope = services.BuildServiceProvider(false).CreateScope())
            {
                var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
                action(runner);
            }
        }

        private static void Resolve(Options options)
        {
            if (options.Type == MigrationType.Up)
            {
                Run(runner =>  MigrateUp(options.Version, runner));
            }
            else
            {
                Run(runner => MigrateDown(options.Version, runner));
            }
        }
        
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args);
            if (options is Parsed<Options> parsed)
            {
                Resolve(parsed.Value);
            }
            else if(options is NotParsed<Options> notParsed)
            {
                throw new ArgumentException(notParsed.Errors.Aggregate(new StringBuilder(), (builder, error) => builder.AppendLine(error.ToString()), builder => builder.ToString()));
            }
        }
    }
}