using System.Threading.Tasks;
using WeatherForecast.CSharp.Domain.Exceptions;

namespace WeatherForecast.CSharp.Domain
{
    public class AccountService : IAccountService
    {
        private readonly IStorageService<User, string> _storageService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;

        public AccountService(IStorageService<User, string> storageService, IAuthenticationService authenticationService, IEncryptionService encryptionService)
        {
            _storageService = storageService;
            _authenticationService = authenticationService;
            _encryptionService = encryptionService;
        }

        public async Task<AuthenticationData> Login(string login, string password)
        {
            var user = await _storageService.GetAsync(login);

            if(user == null)
                throw new UserNotFoundException(login);

            if(user.Password != _encryptionService.Encrypt(password))
                throw new IncorrectPasswordException();

            return _authenticationService.Authenticate(user);
        }

        public async Task<AuthenticationData> Register(string login, string password, string confirmPassword)
        {
            var user = await _storageService.GetAsync(login);

            if(user != null)
                throw new UserAlreadyRegisteredException(login);

            if(password != confirmPassword)
                throw new PasswordsNotMatchException();

            user = new User
            {
                Login = login,
                Password = _encryptionService.Encrypt(password)
            };

            user = await _storageService.SaveAsync(user);

            return _authenticationService.Authenticate(user);
        }
    }
}