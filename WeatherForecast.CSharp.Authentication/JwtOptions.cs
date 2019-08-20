using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WeatherForecast.CSharp.Authentication
{
    public class JwtOptions
    {
        public JwtOptions(IConfiguration configuration)
        {
            SymmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:Key").Value));
            Issuer = configuration.GetSection("JwtOptions:Issuer").Value;
            Audience = configuration.GetSection("JwtOptions:Audience").Value;
            Lifetime = Convert.ToInt32(configuration.GetSection("JwtOptions:Lifetime").Value);
        }
        
        public string Issuer { get; }
        
        public string Audience { get; }
        
        public int Lifetime { get; }
        
        public SymmetricSecurityKey SymmetricSecurityKey { get; }
    }
}