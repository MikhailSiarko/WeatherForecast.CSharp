using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherForecast.CSharp.Domain
{
    public class Forecast : Identity
    {
        [JsonPropertyName("country")]
        public string CountryCode { get; set; }

        [JsonPropertyName("name")]
        public string City { get; set; }

        public DateTimeOffset Created { get; set; }
        
        public IEnumerable<ForecastItem> Items { get; set; }

        public bool IsValid(DateTimeOffset date)
        {
            return this.Created >= date;
        }
    }
}