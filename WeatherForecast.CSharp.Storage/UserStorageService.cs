using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WeatherForecast.CSharp.Domain;

namespace WeatherForecast.CSharp.Storage
{
    public class UserStorageService : IStorageService<User, string>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserStorageService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> GetAsync(string key)
        {
            var entity = await _dbContext.Users.SingleOrDefaultAsync(u => u.Login == key);

            return _mapper.Map<User>(entity);
        }

        public async Task<User> SaveAsync(User obj)
        {
            var entity = _mapper.Map<UserEntity>(obj);

            await _dbContext.Users.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<User>(entity);
        }
    }
}
