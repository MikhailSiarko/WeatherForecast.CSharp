using System;
using System.Security.Cryptography;
using System.Text;
using WeatherForecast.CSharp.API.Interfaces;

namespace WeatherForecast.CSharp.API.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        public string Encrypt(string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));

            const string localeParameter = "ytrewQ";
            SHA1 sh1 = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(source.Insert(source.Length - 5, localeParameter));
            var hash = sh1.ComputeHash(bytes);

            var stringBuilder = new StringBuilder();
            foreach (var t in hash)
            {
                stringBuilder.Append(t.ToString("x8"));
            }

            return stringBuilder.ToString();
        }
    }
}