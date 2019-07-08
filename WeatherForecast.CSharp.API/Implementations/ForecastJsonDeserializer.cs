using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherForecast.CSharp.API.Database.Entities;
using WeatherForecast.CSharp.API.Interfaces;

namespace WeatherForecast.CSharp.API.Implementations
{
    public class ForecastJsonDeserializer : IForecastDeserializer<string>
    {
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
                        var weathers = new List<Weather>();
                        foreach (var weatherJson in jsonElement.GetProperty("weather").EnumerateArray())
                        {
                            weathers.Add(JsonSerializer.Parse<Weather>(weatherJson.ToString()));
                        }
                        
                        timeItems.Add(new ForecastTimeItem
                        {
                            Main = main,
                            Weathers = weathers,
                            Wind = wind,
                            Time = DateTimeOffset.Parse(jsonElement.GetProperty("dt_txt").ToString()).TimeOfDay
                        });
                    }

                    items.Add(new ForecastItem
                    {
                        ForecastTimeItems = timeItems,
                        Date = section.Key
                    });
                }

                forecast.ForecastItems = items;

                return forecast;
            }
        }
    }
}