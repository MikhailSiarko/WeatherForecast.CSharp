using WeatherForecast.CSharp.API.Database.Entities;
using WeatherForecast.CSharp.API.Types.Dto;

namespace WeatherForecast.CSharp.API.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationDataDto Authenticate(User user);
    }
}