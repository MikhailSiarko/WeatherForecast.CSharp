using System;

namespace WeatherForecast.CSharp.API.Exceptions
{
    public class UserAlreadyRegisteredException : ApplicationException
    {
        public UserAlreadyRegisteredException(string login) : base($"User {login} has been already registered")
        {
        }
    }
}