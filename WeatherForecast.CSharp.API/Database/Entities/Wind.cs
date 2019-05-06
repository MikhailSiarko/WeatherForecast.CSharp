namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class Wind : Entity
    {
        public decimal Speed { get; set; }

        public decimal Degree { get; set; }
    }
}