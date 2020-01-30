using System.Threading.Tasks;

namespace WeatherForecast.CSharp.Domain
{
    public interface IStorageService<TE, in TK> where TE : Identity
    {
        Task<TE> GetAsync(TK key);

        Task<TE> SaveAsync(TE obj);
    }
}