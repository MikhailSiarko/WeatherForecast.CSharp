using AutoMapper;
using WeatherForecast.CSharp.Domain;
using WeatherForecast.CSharp.Storage;

namespace WeatherForecast.CSharp.API.MapperProfiles
{
    class ForecastProfile : Profile
    {
        public ForecastProfile()
        {
            CreateMap<ForecastEntity, Forecast>().ReverseMap();
            CreateMap<ForecastItemEntity, ForecastItem>().ReverseMap();
            CreateMap<ForecastTimeItemEntity, ForecastTimeItem>().ReverseMap();
            CreateMap<MainEntity, Main>().ReverseMap();
            CreateMap<WeatherEntity, Weather>().ReverseMap();
            CreateMap<WindEntity, Wind>().ReverseMap();
        }
    }
}