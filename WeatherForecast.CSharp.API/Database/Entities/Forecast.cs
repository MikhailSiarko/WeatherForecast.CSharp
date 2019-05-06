using System;
using System.Collections.Generic;

namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class Forecast : Entity
    {
        public string CountryCode { get; set; }

        public string Location { get; set; }

        public DateTimeOffset Created { get; set; }

        public IEnumerable<ForecastItem> ForecastItems { get; set; }
    }
}