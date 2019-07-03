using AutoMapper;
using WeatherForecast.CSharp.API.Database.Entities;
using WeatherForecast.CSharp.API.Types.Dto;

namespace WeatherForecast.CSharp.API.MapperProfiles
{
    public class ForecastProfile : Profile
    {
        public ForecastProfile()
        {
            CreateMap<Forecast, ForecastDto>();
            CreateMap<ForecastItem, ForecastItemDto>();
            CreateMap<Main, MainDto>();
            CreateMap<Weather, WeatherDto>();
            CreateMap<Wind, WindDto>();
        }
    }
}