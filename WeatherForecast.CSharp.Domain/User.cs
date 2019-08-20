namespace WeatherForecast.CSharp.Domain
{
    public class User : Identity
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}