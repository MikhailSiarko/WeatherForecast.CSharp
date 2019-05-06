using System;

namespace WeatherForecast.CSharp.API.Exceptions
{
    public class PasswordsNotMatchException : ApplicationException
    {
        public PasswordsNotMatchException() : base("Passwords do not match")
        {
        }
    }
}