using System;
using System.Collections.Generic;

namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class ForecastItemDto : EntityDto
    {
        public int ForecastId { get; set; }

        public IEnumerable<ForecastTimeItemDto> TimeItems { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}