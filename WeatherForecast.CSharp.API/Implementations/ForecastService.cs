using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherForecast.CSharp.API.Database;
using WeatherForecast.CSharp.API.Database.Entities;
using WeatherForecast.CSharp.API.Exceptions;
using WeatherForecast.CSharp.API.Infrastructure;
using WeatherForecast.CSharp.API.Interfaces;
using WeatherForecast.CSharp.API.Types.Dto;

namespace WeatherForecast.CSharp.API.Implementations
{
    public class ForecastService : IForecastService
    {
        private readonly AppDbContext _dbContext;
        private readonly int _expirationTime;
        private readonly string _appId;
        private readonly IForecastDeserializer<string> _deserializer;
        private readonly string _forecastUrl;
        private readonly IMapper _mapper;

        public ForecastService(AppDbContext dbContext, IConfiguration configuration, IForecastDeserializer<string> deserializer, IMapper mapper)
        {
            _dbContext = dbContext;
            _deserializer = deserializer;
            _mapper = mapper;
            _forecastUrl = configuration.GetValue<string>("ForecastUrl");
            _expirationTime = configuration.GetValue<int>("ExpirationTime");
            _appId = configuration.GetValue<string>("WeatherForecastServiceApiKey");
        }

        public async Task<ForecastDto> GetForecastAsync(string city)
        {
            var forecast = await _dbContext.Forecasts
                .Include(_dbContext.GetIncludePaths<Forecast>())
                .SingleOrDefaultAsync(f => f.City == city);
            
            if (forecast != null && ForecastIsValid(forecast))
            {
                return _mapper.Map<Forecast, ForecastDto>(forecast);
            }
            
            var newForecast = await FetchForecast(city);
            forecast = await UpdateOrAddForecast(forecast, newForecast);
            return _mapper.Map<Forecast, ForecastDto>(forecast);
        }

        private async Task<Forecast> UpdateOrAddForecast(Forecast forecast, Forecast newForecast)
        {
            if (forecast == null)
            {
                newForecast.Created = DateTimeOffset.Now;
                _dbContext.Forecasts.Add(newForecast);
                await _dbContext.SaveChangesAsync();
                return newForecast;
            }
            
            _dbContext.ForecastItems.RemoveRange(forecast.Items);
            forecast.Items = newForecast.Items; 
            forecast.Created = DateTimeOffset.Now;
            await _dbContext.SaveChangesAsync();
            return forecast;
        }

        private async Task<Forecast> FetchForecast(string city)
        {
            using (var httpClient = new HttpClient())
            {
                var response  = await httpClient.GetAsync(string.Format(_forecastUrl, city, _appId));
            
                if (!response.IsSuccessStatusCode)
                {
                    throw new SomethingWrongException();
                }

                var json = await response.Content.ReadAsStringAsync();

                return _deserializer.Deserialize(json);
            }
        }

        private bool ForecastIsValid(Forecast forecast)
        {
            return forecast.Created >= DateTimeOffset.Now.AddMinutes(-1 * _expirationTime);
        }
    }
}