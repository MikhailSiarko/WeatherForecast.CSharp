using System.Text.Json.Serialization;

namespace WeatherForecast.CSharp.Domain
{
    public class Wind : ForecastTimeItemIdentity
    {
        [JsonPropertyName("speed")]
        public decimal Speed { get; set; }

        [JsonPropertyName("deg")]
        public decimal Degree { get; set; }
    }
}