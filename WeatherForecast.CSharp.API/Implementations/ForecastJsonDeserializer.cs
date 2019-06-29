using System;
using System.Collections.Generic;
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

                foreach (var section in itemsJson.EnumerateArray())
                {
                    var main = JsonSerializer.Parse<Main>(section.GetProperty("main").ToString());
                    var wind = JsonSerializer.Parse<Wind>(section.GetProperty("wind").ToString());
                    var weathers = new List<Weather>();
                    foreach (var weatherJson in section.GetProperty("weather").EnumerateArray())
                    {
                        weathers.Add(JsonSerializer.Parse<Weather>(weatherJson.ToString()));
                    }

                    items.Add(new ForecastItem
                    {
                        Weathers = weathers,
                        Main = main,
                        Wind = wind,
                        Date = DateTimeOffset.Parse(section.GetProperty("dt_txt").GetString())
                    });
                }

                forecast.ForecastItems = items;

                return forecast;
            }
        }
    }
}