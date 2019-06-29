using System;
using System.Collections.Generic;

namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class ForecastItem : Entity
    {
        public int ForecastId { get; set; }

        public virtual Main Main { get; set; }
        
        public virtual IEnumerable<Weather> Weathers { get; set; }

        public virtual Wind Wind { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}