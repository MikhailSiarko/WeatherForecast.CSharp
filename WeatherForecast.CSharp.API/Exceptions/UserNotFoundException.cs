using System;

namespace WeatherForecast.CSharp.API.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string login) : base($"User {login} wasn't found")
        {
        }
    }
}