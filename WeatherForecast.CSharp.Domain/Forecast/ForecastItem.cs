using System;
using System.Collections.Generic;

namespace WeatherForecast.CSharp.Domain
{
    public class ForecastItem : Identity
    {
        public int ForecastId { get; set; }

        public IEnumerable<ForecastTimeItem> TimeItems { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}