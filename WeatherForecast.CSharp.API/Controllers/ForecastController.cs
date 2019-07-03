using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.CSharp.API.Interfaces;

namespace WeatherForecast.CSharp.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ForecastController : Controller
    {
        private readonly IForecastService _forecastService;
        
        public ForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get([FromRoute] string city)
        {
            return Ok(await _forecastService.GetForecastAsync(city));
        }
    }
}