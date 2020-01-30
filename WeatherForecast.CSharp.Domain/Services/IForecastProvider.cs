using System.Threading.Tasks;

namespace WeatherForecast.CSharp.Domain
{
    public interface IForecastProvider
    {
        Task<Forecast> FetchForecastAsync(string location);
    }
}