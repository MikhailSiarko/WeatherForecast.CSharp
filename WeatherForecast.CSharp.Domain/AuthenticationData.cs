namespace WeatherForecast.CSharp.Domain
{
    public class AuthenticationData
    {
        public AuthenticationData(string token, UserInfo user)
        {
            Token = token;
            User = user;
        }
        
        public string Token { get; }

        public UserInfo User { get; }
    }
}
