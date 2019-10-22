using System;
using System.Collections.Generic;

namespace WeatherForecast.CSharp.Storage
{
    public class ForecastEntity : Entity
    {
        public string CountryCode { get; set; }
        
        public string City { get; set; }

        public DateTimeOffset Created { get; set; }
        
        public IEnumerable<ForecastItemEntity> Items { get; set; }
    }
}