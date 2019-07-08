namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class WindDto : ForecastTimeItemEntityDto
    {
        public decimal Speed { get; set; }

        public decimal Degree { get; set; }
    }
}