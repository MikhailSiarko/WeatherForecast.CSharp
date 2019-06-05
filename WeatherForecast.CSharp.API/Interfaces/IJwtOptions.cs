using Microsoft.IdentityModel.Tokens;

namespace WeatherForecast.CSharp.API.Interfaces
{
    public interface IJwtOptions
    {
        string Issuer { get; }

        string Audience { get; }

        int Lifetime { get; }

        SymmetricSecurityKey SymmetricSecurityKey { get; }
    }
}