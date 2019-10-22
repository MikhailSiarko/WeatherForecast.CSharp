using System;

namespace WeatherForecast.CSharp.Domain.Exceptions
{
    public class SomethingWrongException : ApplicationException
    {
        public SomethingWrongException(string message) : base("Something went wrong: " + message)
        {
        }

        public SomethingWrongException() : base("Something went wrong")
        {
        }
    }
}