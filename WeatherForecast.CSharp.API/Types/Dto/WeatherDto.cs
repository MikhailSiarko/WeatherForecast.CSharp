namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class WeatherDto : ForecastTimeItemEntityDto
    {
        public string Main { get; set; }

        public string Description { get; set; }
    }
}