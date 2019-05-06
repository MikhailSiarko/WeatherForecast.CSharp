using System.Threading.Tasks;
using WeatherForecast.CSharp.API.Types.Dto;

namespace WeatherForecast.CSharp.API.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticationDataDto> Login(string login, string password);
        
        Task<AuthenticationDataDto> Register(string login, string password, string confirmPassword);
    }
}