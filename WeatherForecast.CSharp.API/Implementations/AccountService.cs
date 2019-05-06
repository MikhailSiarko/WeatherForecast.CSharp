using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.CSharp.API.Database;
using WeatherForecast.CSharp.API.Database.Entities;
using WeatherForecast.CSharp.API.Exceptions;
using WeatherForecast.CSharp.API.Interfaces;
using WeatherForecast.CSharp.API.Types.Dto;

namespace WeatherForecast.CSharp.API.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;

        public AccountService(AppDbContext dbContext, IAuthenticationService authenticationService, IEncryptionService encryptionService)
        {
            _dbContext = dbContext;
            _authenticationService = authenticationService;
            _encryptionService = encryptionService;
        }

        public async Task<AuthenticationDataDto> Login(string login, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Login == login);
            if(user == null)
                throw new UserNotFoundException(login);
            if(user.Password != _encryptionService.Encrypt(password))
                throw new IncorrectPasswordException();
            return _authenticationService.Authenticate(user);
        }

        public async Task<AuthenticationDataDto> Register(string login, string password, string confirmPassword)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Login == login);
            if(user != null)
                throw new UserAlreadyRegisteredException(login);
            if(password != confirmPassword)
                throw new PasswordsNotMatchException();
            user = new User
            {
                Login = login,
                Password = _encryptionService.Encrypt(password)
            };
            await _dbContext.AddAsync(user);
            return _authenticationService.Authenticate(user);
        }
    }
}