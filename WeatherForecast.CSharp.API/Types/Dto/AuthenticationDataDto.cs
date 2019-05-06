namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class AuthenticationDataDto
    {
        public AuthenticationDataDto(string token, UserDto user)
        {
            Token = token;
            User = user;
        }
        
        public string Token { get; }

        public UserDto User { get; }
    }
}