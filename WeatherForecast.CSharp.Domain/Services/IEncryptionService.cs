namespace WeatherForecast.CSharp.Domain
{
    public interface IEncryptionService
    {
        string Encrypt(string source);
    }
}