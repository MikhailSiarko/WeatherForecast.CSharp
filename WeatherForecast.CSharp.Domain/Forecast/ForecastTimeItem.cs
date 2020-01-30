using System;

namespace WeatherForecast.CSharp.Domain
{
    public class ForecastTimeItem : Identity
    {
        public int ForecastItemId { get; set; }

        public DateTimeOffset Time { get; set; }
        
        public virtual Main Main { get; set; }
        
        public virtual Weather Weather { get; set; }

        public virtual Wind Wind { get; set; }
    }
}