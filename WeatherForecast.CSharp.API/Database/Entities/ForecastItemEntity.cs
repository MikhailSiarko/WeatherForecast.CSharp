namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class ForecastItemEntity : Entity
    {
        public int ForecastItemId { get; set; }

        public virtual ForecastItem ForecastItem { get; set; }
    }
}