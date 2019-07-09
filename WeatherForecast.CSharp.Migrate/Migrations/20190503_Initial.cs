using FluentMigrator;

namespace WeatherForecast.CSharp.Migrate.Migrations
{
    [Migration(20190503011700)]
    public class Initial : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().Indexed("IX_Users_Id").Identity().PrimaryKey("PK_Users").NotNullable()
                .WithColumn("Login").AsFixedLengthString(30).Unique().NotNullable()
                .WithColumn("Password").AsString().NotNullable();

            Create
                .Table("Forecasts")
                .WithColumn("Id").AsInt32().Indexed("IX_Forecasts_Id").Identity().PrimaryKey("PK_Forecasts")
                .NotNullable()
                .WithColumn("CountryCode").AsFixedLengthString(10).NotNullable()
                .WithColumn("Location").AsFixedLengthString(100).NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable();

            Create.Table("ForecastItems")
                .WithColumn("Id").AsInt32().Indexed("IX_ForecastItems_Id").Identity().PrimaryKey("PK_ForecastItems")
                .NotNullable()
                .WithColumn("ForecastId").AsInt32().Indexed("IX_ForecastItems_ForecastId")
                .ForeignKey("FK_ForecastItems_Forecasts_Id", "Forecasts", "Id").NotNullable()
                .WithColumn("Date").AsDateTime().NotNullable();
            
            Create.Table("ForecastTimeItems")
                .WithColumn("Id").AsInt64().Indexed("IX_ForecastTimeItems_Id").Identity().PrimaryKey("PK_ForecastTimeItems").NotNullable()
                .WithColumn("Time").AsDateTime().NotNullable()
                .WithColumn("ForecastItemId").AsInt64().Indexed("IX_Mains_ForecastItemId")
                .ForeignKey("FK_Mains_ForecastItems_Id", "ForecastItems", "Id").NotNullable();

            Create.Table("Mains")
                .WithColumn("Id").AsInt64().Indexed("IX_Mains_Id").Identity().PrimaryKey("PK_Mains").NotNullable()
                .WithColumn("Temp").AsDecimal().NotNullable()
                .WithColumn("MinTemp").AsDecimal().NotNullable()
                .WithColumn("MaxTemp").AsDecimal().NotNullable()
                .WithColumn("Pressure").AsDecimal().NotNullable()
                .WithColumn("Humidity").AsInt32().NotNullable()
                .WithColumn("ForecastTimeItemId").AsInt64().Indexed("IX_Mains_ForecastTimeItemId")
                .ForeignKey("FK_Mains_ForecastTimeItems_Id", "ForecastTimeItems", "Id").NotNullable();

            Create
                .Table("Weathers")
                .WithColumn("Id").AsInt64().Indexed("IX_Weathers_Id").Identity().PrimaryKey("PK_Weathers").NotNullable()
                .WithColumn("Main").AsFixedLengthString(50).NotNullable()
                .WithColumn("Description").AsFixedLengthString(100).NotNullable()
                .WithColumn("Icon").AsString().NotNullable()
                .WithColumn("ForecastTimeItemId").AsInt64().Indexed("IX_Weathers_ForecastTimeItemId")
                .ForeignKey("FK_Weathers_ForecastTimeItems_Id", "ForecastTimeItems", "Id").NotNullable();

            Create.Table("Winds")
                .WithColumn("Id").AsInt64().Indexed("IX_Winds_Id").Identity().PrimaryKey("PK_Winds").NotNullable()
                .WithColumn("Speed").AsDecimal().NotNullable()
                .WithColumn("Degree").AsDecimal().NotNullable()
                .WithColumn("ForecastTimeItemId").AsInt64().Indexed("IX_Winds_ForecastTimeItemId")
                .ForeignKey("FK_Winds_ForecastTimeItems_Id", "ForecastTimeItems", "Id").NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ForecastItems");
            Delete.Table("ForecastTimeItems");
            Delete.Table("Forecasts");
            Delete.Table("Users");
            Delete.Table("Mains");
            Delete.Table("Weathers");
            Delete.Table("Winds");
        }
    }
}