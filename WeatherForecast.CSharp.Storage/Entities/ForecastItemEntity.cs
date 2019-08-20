using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.CSharp.Storage
{
    public class ForecastItemEntity : Entity
    {
        [ForeignKey("Forecast")]
        public int ForecastId { get; set; }

        public IEnumerable<ForecastTimeItemEntity> TimeItems { get; set; }

        public DateTimeOffset Date { get; set; }
        
        public ForecastEntity Forecast { get; set; }
    }
}