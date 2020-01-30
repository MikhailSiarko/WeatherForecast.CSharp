using System;

namespace WeatherForecast.CSharp.Domain.Exceptions
{
    public class IncorrectPasswordException : ApplicationException
    {
        public IncorrectPasswordException() : base("You've entered an incorrect password")
        {
        }
    }
}