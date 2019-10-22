using System.Threading.Tasks;

namespace WeatherForecast.CSharp.Domain
{
    public interface IAccountService
    {
        Task<AuthenticationData> Login(string login, string password);
        
        Task<AuthenticationData> Register(string login, string password, string confirmPassword);
    }
}