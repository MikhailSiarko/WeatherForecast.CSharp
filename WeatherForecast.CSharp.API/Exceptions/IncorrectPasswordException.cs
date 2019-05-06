using System;

namespace WeatherForecast.CSharp.API.Exceptions
{
    public class IncorrectPasswordException : ApplicationException
    {
        public IncorrectPasswordException() : base("You've entered an incorrect password")
        {
        }
    }
}