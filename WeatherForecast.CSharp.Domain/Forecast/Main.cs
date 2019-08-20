using System.Text.Json.Serialization;

namespace WeatherForecast.CSharp.Domain
{
    public class Main : ForecastTimeItemIdentity
    {
        [JsonPropertyName("temp")]
        public decimal Temp { get; set; }

        [JsonPropertyName("temp_max")]
        public decimal MaxTemp { get; set; }
        
        [JsonPropertyName("temp_min")]
        public decimal MinTemp { get; set; }

        [JsonPropertyName("pressure")]
        public decimal Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }
}