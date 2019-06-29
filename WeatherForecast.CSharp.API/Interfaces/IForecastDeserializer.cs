using WeatherForecast.CSharp.API.Database.Entities;

namespace WeatherForecast.CSharp.API.Interfaces
{
    public interface IForecastDeserializer<in T>
    {
        Forecast Deserialize(T source);
    }
}