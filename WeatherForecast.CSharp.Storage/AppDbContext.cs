using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.CSharp.Storage
{
    public class AppDbContext : DbContext
    {
        public DbSet<ForecastEntity> Forecasts { get; set; }
        public DbSet<ForecastItemEntity> ForecastItems { get; set; }
        public DbSet<ForecastTimeItemEntity> ForecastTimeItems { get; set; }
        public DbSet<MainEntity> Mains { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<WeatherEntity> Weathers { get; set; }
        public DbSet<WindEntity> Winds { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}