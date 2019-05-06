using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WeatherForecast.CSharp.API.Interfaces;

namespace WeatherForecast.CSharp.API.Implementations
{
    public class JwtOptions : IJwtOptions
    {
        private readonly string _key;

        public JwtOptions(IConfiguration configuration)
        {
            _key = configuration.GetValue<string>("Key");
            Issuer = configuration.GetValue<string>(nameof(Issuer));
            Audience = configuration.GetValue<string>(nameof(Audience));
            Lifetime = configuration.GetValue<int>(nameof(Lifetime));
        }
        
        public string Issuer { get; }
        
        public string Audience { get; }
        
        public int Lifetime { get; }
        
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
        }
    }
}