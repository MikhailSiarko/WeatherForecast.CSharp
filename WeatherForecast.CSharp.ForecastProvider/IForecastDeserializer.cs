using WeatherForecast.CSharp.Domain;

namespace WeatherForecast.CSharp.ForecastProvider
{
    public interface IForecastDeserializer<in T>
    {
        Forecast Deserialize(T source);
    }
}