namespace WeatherForecast.CSharp.API.Types.Dto
{
    public class MainDto : ForecastItemEntityDto
    {
        public decimal Temp { get; set; }

        public decimal MaxTemp { get; set; }
        
        public decimal MinTemp { get; set; }

        public decimal Pressure { get; set; }

        public int Humidity { get; set; }
    }
}