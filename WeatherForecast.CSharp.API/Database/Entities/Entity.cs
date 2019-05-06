using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.CSharp.API.Database.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}