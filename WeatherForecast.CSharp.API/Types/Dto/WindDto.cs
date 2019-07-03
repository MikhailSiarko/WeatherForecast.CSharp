namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class WindDto : ForecastItemEntityDto
    {
        public decimal Speed { get; set; }

        public decimal Degree { get; set; }
    }
}