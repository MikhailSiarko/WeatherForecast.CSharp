using Microsoft.EntityFrameworkCore;
using WeatherForecast.CSharp.API.Database.Entities;

namespace WeatherForecast.CSharp.API.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Forecast> Forecasts { get; set; }
        public DbSet<ForecastItem> ForecastItems { get; set; }
        public DbSet<ForecastTimeItem> ForecastTimeItems { get; set; }
        public DbSet<Main> Mains { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<Wind> Winds { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}