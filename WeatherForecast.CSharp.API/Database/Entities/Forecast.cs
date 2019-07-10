using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class Forecast : Entity
    {
        [JsonPropertyName("country")]
        public string CountryCode { get; set; }

        [JsonPropertyName("name")]
        public string City { get; set; }

        public DateTimeOffset Created { get; set; }
        
        public IEnumerable<ForecastItem> Items { get; set; }
    }
}