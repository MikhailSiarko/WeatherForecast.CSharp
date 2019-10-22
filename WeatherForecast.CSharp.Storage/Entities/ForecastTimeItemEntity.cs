using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.CSharp.Storage
{
    public class ForecastTimeItemEntity : Entity
    {
        [ForeignKey("ForecastItem")]
        public int ForecastItemId { get; set; }

        public DateTimeOffset Time { get; set; }
        
        public MainEntity Main { get; set; }
        
        public WeatherEntity Weather { get; set; }

        public WindEntity Wind { get; set; }

        public ForecastItemEntity ForecastItem { get; set; }
    }
}