using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.CSharp.Storage
{
    public abstract class TimeItemEntity : Entity
    {
        [ForeignKey("ForecastTimeItem")]
        public int ForecastTimeItemId { get; set; }

        public ForecastTimeItemEntity ForecastTimeItem { get; set; }
    }
}