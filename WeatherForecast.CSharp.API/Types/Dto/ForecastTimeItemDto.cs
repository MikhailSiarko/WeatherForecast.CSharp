using System;

namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class ForecastTimeItemDto : EntityDto
    {
        public int ForecastItemId { get; set; }
        
        public MainDto Main { get; set; }
        
        public WeatherDto Weather { get; set; }

        public WindDto Wind { get; set; }

        public DateTime Time { get; set; }
    }
}