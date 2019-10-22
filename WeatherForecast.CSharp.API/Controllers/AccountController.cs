using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.CSharp.API.RequestData;
using WeatherForecast.CSharp.Domain;

namespace WeatherForecast.CSharp.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            var authenticationData =
                await _accountService.Login(loginData.Login, loginData.Password);

            return Ok(authenticationData);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterData registerData)
        {
            var authenticationData = await _accountService.Register(registerData.Login, registerData.Password,
                registerData.ConfirmPassword);

            return Ok(authenticationData);
        }
    }
}