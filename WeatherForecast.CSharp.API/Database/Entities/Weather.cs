namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class Weather : Entity
    {
        public string  Main { get; set; }

        public string Description { get; set; }
    }
}