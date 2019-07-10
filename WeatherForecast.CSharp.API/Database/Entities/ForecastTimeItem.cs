using System;

namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class ForecastTimeItem : Entity
    {
        public int ForecastItemId { get; set; }

        public DateTimeOffset Time { get; set; }
        
        public virtual Main Main { get; set; }
        
        public virtual Weather Weather { get; set; }

        public virtual Wind Wind { get; set; }
    }
}