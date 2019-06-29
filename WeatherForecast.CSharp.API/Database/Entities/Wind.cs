using System.Text.Json.Serialization;

namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class Wind : ForecastItemEntity
    {
        [JsonPropertyName("speed")]
        public decimal Speed { get; set; }

        [JsonPropertyName("deg")]
        public decimal Degree { get; set; }
    }
}