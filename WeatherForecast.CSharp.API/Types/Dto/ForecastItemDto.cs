using System;
using System.Collections.Generic;

namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class ForecastItemDto : EntityDto
    {
        public int ForecastId { get; set; }

        public virtual MainDto Main { get; set; }
        
        public virtual IEnumerable<WeatherDto> Weathers { get; set; }

        public virtual WindDto Wind { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}