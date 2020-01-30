using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WeatherForecast.CSharp.Domain;

namespace WeatherForecast.CSharp.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtOptions _jwtOptions;
        
        public AuthenticationService(JwtOptions options)
        {
            _jwtOptions = options;
        }
        
        public AuthenticationData Authenticate(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var token = EncodeSecurityToken(user);
            var userInfo = new UserInfo { Id = user.Id, Login = user.Login };
            return new AuthenticationData(token, userInfo);
        }
        
        private string EncodeSecurityToken(User user)
        {
            var claimsIdentity = GetIdentity(user);
            var jwt = GenerateToken(claimsIdentity.Claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwt);
        }

        private static ClaimsIdentity GetIdentity(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("Id", user.Id.ToString())
            };

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            return new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(_jwtOptions.Lifetime)),
                signingCredentials: new SigningCredentials(_jwtOptions.SymmetricSecurityKey,
                    SecurityAlgorithms.HmacSha256));
        }
    }
}