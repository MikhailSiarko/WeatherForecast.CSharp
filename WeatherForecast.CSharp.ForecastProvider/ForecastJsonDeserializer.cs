using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WeatherForecast.CSharp.Domain;

namespace WeatherForecast.CSharp.ForecastProvider
{
    public class ForecastJsonDeserializer : IForecastDeserializer<string>
    {
        private readonly IConfiguration _configuration;
        
        public ForecastJsonDeserializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Forecast Deserialize(string source)
        {
            using var document = JsonDocument.Parse(source);
            
            var forecastJson = document.RootElement.GetProperty("city");
                
            var itemsJson = document.RootElement.GetProperty("list");

            var forecast = JsonSerializer.Deserialize<Forecast>(forecastJson.ToString() ?? string.Empty);

            var items = new List<ForecastItem>();

            foreach (var section in itemsJson.EnumerateArray().GroupBy(x => DateTimeOffset.Parse(x.GetProperty("dt_txt").ToString()).Date))
            {
                var timeItems = new List<ForecastTimeItem>();
                foreach (var jsonElement in section)
                {
                    var main = JsonSerializer.Deserialize<Main>(jsonElement.GetProperty("main").ToString() ?? string.Empty);
                    var wind = JsonSerializer.Deserialize<Wind>(jsonElement.GetProperty("wind").ToString() ?? string.Empty);
                    var weatherJson = jsonElement.GetProperty("weather").EnumerateArray().First();
                    var weather = JsonSerializer.Deserialize<Weather>(weatherJson.ToString() ?? string.Empty);
                    weather.Icon = string.Format(_configuration.GetSection("WeatherAPI:IconUrlFormat").Value, weather.Icon);
                    timeItems.Add(new ForecastTimeItem
                    {
                        Main = main,
                        Weather = weather,
                        Wind = wind,
                        Time = DateTimeOffset.Parse(jsonElement.GetProperty("dt_txt").ToString() ?? string.Empty)
                    });
                }

                items.Add(new ForecastItem
                {
                    TimeItems = timeItems,
                    Date = section.Key
                });
            }
            
            if(forecast != null)
                forecast.Items = items;

            return forecast;
        }
    }
}