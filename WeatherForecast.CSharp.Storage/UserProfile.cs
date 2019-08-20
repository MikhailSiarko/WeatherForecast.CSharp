using AutoMapper;
using WeatherForecast.CSharp.Domain;

namespace WeatherForecast.CSharp.Storage
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, User>().ReverseMap();
        }
    }
}
