using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WeatherForecast.CSharp.Domain
{
    public class ForecastService : IForecastService
    {
        private readonly IForecastProvider _forecastProvider;
        private readonly IStorageService<Forecast, string> _storageService;
        private readonly int _expirationTime;

        public ForecastService(IForecastProvider forecastProvider, IStorageService<Forecast, string> storageService, IConfiguration configuration)
        {
            _forecastProvider = forecastProvider;
            _storageService = storageService;
            _expirationTime = Convert.ToInt32(configuration.GetSection("ExpirationTime").Value);
        }

        public async Task<Forecast> GetForecastAsync(string location)
        {
            var forecast = await _storageService.GetAsync(location);
            
            if (forecast != null && forecast.IsValid(DateTimeOffset.Now.AddMinutes(-1 * _expirationTime)))
            {
                return forecast;
            }
            
            var newForecast = await _forecastProvider.FetchForecastAsync(location);
            return await _storageService.SaveAsync(newForecast);
        }
    }
}