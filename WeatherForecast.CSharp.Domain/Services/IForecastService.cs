using System.Threading.Tasks;

namespace WeatherForecast.CSharp.Domain
{
    public interface IForecastService
    {
        Task<Forecast> GetForecastAsync(string location);
    }
}