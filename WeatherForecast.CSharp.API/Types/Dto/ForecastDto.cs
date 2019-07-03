using System;
using System.Collections.Generic;
using WeatherForecast.CSharp.API.Database.Entities;

namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class ForecastDto : EntityDto
    {
        public string CountryCode { get; set; }

        public string Location { get; set; }

        public DateTimeOffset Created { get; set; }
        
        public IEnumerable<ForecastItem> ForecastItems { get; set; }
    }
}