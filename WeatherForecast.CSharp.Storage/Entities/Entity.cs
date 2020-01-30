using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.CSharp.Storage
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}