namespace WeatherForecast.CSharp.API.Database.Entities
{
    public class User : Entity
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}