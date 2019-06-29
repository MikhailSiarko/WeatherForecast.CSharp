using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WeatherForecast.CSharp.API.Interfaces;

namespace WeatherForecast.CSharp.API.Implementations
{
    public class JwtOptions : IJwtOptions
    {
        public JwtOptions(IConfiguration configuration)
        {
            SymmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtOptions:Key")));
            Issuer = configuration.GetValue<string>("JwtOptions:Issuer");
            Audience = configuration.GetValue<string>("JwtOptions:Audience");
            Lifetime = configuration.GetValue<int>("JwtOptions:Lifetime");
        }
        
        public string Issuer { get; }
        
        public string Audience { get; }
        
        public int Lifetime { get; }
        
        public SymmetricSecurityKey SymmetricSecurityKey { get; }
    }
}