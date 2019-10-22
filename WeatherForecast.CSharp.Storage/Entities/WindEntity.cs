namespace WeatherForecast.CSharp.Storage
{
    public class WindEntity : TimeItemEntity
    {
        public decimal Speed { get; set; }

        public decimal Degree { get; set; }
    }
}