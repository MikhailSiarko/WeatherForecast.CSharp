using System;
using System.Collections.Generic;

namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class ForecastTimeItemDto : EntityDto
    {
        public int ForecastItemId { get; set; }
        
        public virtual MainDto Main { get; set; }
        
        public virtual IEnumerable<WeatherDto> Weathers { get; set; }

        public virtual WindDto Wind { get; set; }

        public DateTime Time { get; set; }
    }
}