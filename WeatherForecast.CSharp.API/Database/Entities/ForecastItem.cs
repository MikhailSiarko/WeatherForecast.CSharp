using System;
using System.Collections.Generic;

namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class ForecastItem : Entity
    {
        public int ForecastId { get; set; }

        public IEnumerable<ForecastTimeItem> TimeItems { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}