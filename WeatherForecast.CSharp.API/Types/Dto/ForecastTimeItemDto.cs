using System;

namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class ForecastTimeItemDto : EntityDto
    {
        public int ForecastItemId { get; set; }

        public TimeSpan Time { get; set; }
    }
}