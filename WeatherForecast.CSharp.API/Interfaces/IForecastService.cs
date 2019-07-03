using System.Threading.Tasks;
using WeatherForecast.CSharp.API.Types.Dto;

namespace WeatherForecast.CSharp.API.Interfaces
{
    public interface IForecastService
    {
        Task<ForecastDto> GetForecastAsync(string city);
    }
}