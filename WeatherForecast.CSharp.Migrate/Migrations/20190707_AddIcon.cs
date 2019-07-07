using FluentMigrator;

namespace WeatherForecast.CSharp.Migrate.Migrations
{
    [Migration(20190707011700)]
    public class AddIcon : Migration
    {
        public override void Up()
        {
            Alter
                .Table("Weathers")
                .AddColumn("Icon")
                .AsString()
                .Nullable();
        }

        public override void Down()
        {
            Delete
                .Column("Icon")
                .FromTable("Weathers");
        }
    }
}