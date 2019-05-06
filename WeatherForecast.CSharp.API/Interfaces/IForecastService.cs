using System.Threading.Tasks;
using WeatherForecast.CSharp.API.Database.Entities;

namespace WeatherForecast.CSharp.API.Interfaces
{
    public interface IForecastService
    {
        Task<Forecast> GetForecastAsync(string city);
    }
}