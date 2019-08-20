using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.CSharp.Domain;
using WeatherForecast.CSharp.Domain.Exceptions;

namespace WeatherForecast.CSharp.ForecastProvider
{
    public class WebAPIForecastProvider : IForecastProvider
    {
        private readonly IForecastDeserializer<string> _forecastDeserializer;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _forecastUrl;
        private readonly string _appId;

        public WebAPIForecastProvider(IForecastDeserializer<string> forecastDeserializer, IConfiguration configuration,
            IHttpClientFactory clientFactory)
        {
            _forecastDeserializer = forecastDeserializer;
            _clientFactory = clientFactory;
            _forecastUrl = configuration.GetValue<string>("WeatherAPI:ForecastUrlFormat");
            _appId = configuration.GetValue<string>("WeatherForecastServiceApiKey");
        }

        public async Task<Forecast> FetchForecastAsync(string location)
        {
            using (var httpClient = _clientFactory.CreateClient("weather"))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_forecastUrl, location, _appId));

                var response = await httpClient.SendAsync(request);
            
                if (!response.IsSuccessStatusCode)
                {
                    throw new SomethingWrongException();
                }

                var json = await response.Content.ReadAsStringAsync();

                return _forecastDeserializer.Deserialize(json);
            }
        }
    }
}
