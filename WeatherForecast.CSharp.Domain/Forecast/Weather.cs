using System.Text.Json.Serialization;

namespace WeatherForecast.CSharp.Domain
{
    public class Weather : ForecastTimeItemIdentity
    {
        [JsonPropertyName("main")]
        public string Main { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}