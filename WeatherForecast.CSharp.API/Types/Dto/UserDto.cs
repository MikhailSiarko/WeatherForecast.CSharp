namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class UserDto
    {
        public UserDto(int id, string login)
        {
            Id = id;
            Login = login;
        }

        public int Id { get; }

        public string Login { get; }
    }
}