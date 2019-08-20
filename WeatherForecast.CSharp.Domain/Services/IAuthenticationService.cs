namespace WeatherForecast.CSharp.Domain
{
    public interface IAuthenticationService
    {
        AuthenticationData Authenticate(User user);
    }
}