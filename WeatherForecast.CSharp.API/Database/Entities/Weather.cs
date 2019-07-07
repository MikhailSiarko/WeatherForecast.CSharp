using System.Text.Json.Serialization;

namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class Weather : ForecastItemEntity
    {
        [JsonPropertyName("main")]
        public string Main { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}