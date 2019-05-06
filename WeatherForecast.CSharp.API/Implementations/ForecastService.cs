using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherForecast.CSharp.API.Database;
using WeatherForecast.CSharp.API.Database.Entities;
using WeatherForecast.CSharp.API.Interfaces;

namespace WeatherForecast.CSharp.API.Implementations
{
    public class ForecastService : IForecastService
    {
        private readonly AppDbContext _dbContext;
        private readonly int _expirationTime;

        public ForecastService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _expirationTime = configuration.GetValue<int>("ExpirationTime");
        }

        public async Task<Forecast> GetForecastAsync(string city)
        {
            var forecast = await _dbContext.Forecasts.SingleOrDefaultAsync(f => f.Location == city);
            if (forecast != null && ForecastIsValid(forecast))
            {
                return forecast;
            }
            
            var items = await FetchForecastItems(city);
            await UpdateForecast(forecast, items);
            return forecast;
        }

        private async Task UpdateForecast(Forecast forecast, IEnumerable<ForecastItem> items)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<ForecastItem>> FetchForecastItems(string city)
        {
            throw new NotImplementedException();
        }

        private bool ForecastIsValid(Forecast forecast)
        {
            return forecast.Created >= DateTimeOffset.Now.AddMinutes(-1 * _expirationTime);
        }
    }
}