namespace WeatherForecast.CSharp.Storage
{
    public class WeatherEntity : TimeItemEntity
    {
        public string Main { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }
    }
}