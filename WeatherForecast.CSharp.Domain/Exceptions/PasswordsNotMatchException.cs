using System;

namespace WeatherForecast.CSharp.Domain.Exceptions
{
    public class PasswordsNotMatchException : ApplicationException
    {
        public PasswordsNotMatchException() : base("Passwords do not match")
        {
        }
    }
}