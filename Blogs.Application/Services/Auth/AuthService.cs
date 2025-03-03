using Blogs.Application.Exceptions;
using Blogs.Application.Models;
using Blogs.Application.Services.Users;

namespace Blogs.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(IUserService userService, JwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }
        public async Task<string> RegisterAsync(User user, CancellationToken cancellationToken)
        {
            var userExist = await _userService.ExistByEmailAsync(user.Email, cancellationToken);

            if (userExist)
            {
                throw new UserAlreadyExistsException(user.Email);
            }
            
            var isUserCreated = await _userService.CreateAsync(user, cancellationToken);

            if (!isUserCreated)
            {
                throw new InvalidCredentialsException();
            }

            var token = _jwtTokenService.GenerateToken(user);

            return token;
        }
        public Task<string> LoginAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    


    }
}
