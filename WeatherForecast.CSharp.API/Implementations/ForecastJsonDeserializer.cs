using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using WeatherForecast.CSharp.API.Database.Entities;
using WeatherForecast.CSharp.API.Interfaces;

namespace WeatherForecast.CSharp.API.Implementations
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
            using (var document = JsonDocument.Parse(source))
            {
                var forecastJson = document.RootElement.GetProperty("city");
                
                var itemsJson = document.RootElement.GetProperty("list");

                var forecast = JsonSerializer.Parse<Forecast>(forecastJson.ToString());

                var items = new List<ForecastItem>();

                foreach (var section in itemsJson.EnumerateArray().GroupBy(x => DateTimeOffset.Parse(x.GetProperty("dt_txt").ToString()).Date))
                {
                    var timeItems = new List<ForecastTimeItem>();
                    foreach (var jsonElement in section.ToList())
                    {
                        var main = JsonSerializer.Parse<Main>(jsonElement.GetProperty("main").ToString());
                        var wind = JsonSerializer.Parse<Wind>(jsonElement.GetProperty("wind").ToString());
                        var weatherJson = jsonElement.GetProperty("weather").EnumerateArray().First();
                        var weather = JsonSerializer.Parse<Weather>(weatherJson.ToString());
                        weather.Icon = string.Format(_configuration.GetSection("IconUrlFormat").Value, weather.Icon);
                        timeItems.Add(new ForecastTimeItem
                        {
                            Main = main,
                            Weather = weather,
                            Wind = wind,
                            Time = DateTimeOffset.Parse(jsonElement.GetProperty("dt_txt").ToString())
                        });
                    }

                    items.Add(new ForecastItem
                    {
                        TimeItems = timeItems,
                        Date = section.Key
                    });
                }

                forecast.Items = items;

                return forecast;
            }
        }
    }
}