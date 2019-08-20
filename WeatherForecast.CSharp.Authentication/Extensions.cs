using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WeatherForecast.CSharp.Authentication
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    var jwtOptions = services.BuildServiceProvider(false).GetService<JwtOptions>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = jwtOptions.SymmetricSecurityKey
                    };
                });

            return services;
        }
    }
}
