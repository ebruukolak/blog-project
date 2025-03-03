using Blogs.Application.Exceptions;
using Blogs.Application.Models;
using Blogs.Application.Services.Email;
using Blogs.Application.Services.Users;

namespace Blogs.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IEmailVerificationService _emailVerificationService;
        public AuthService(IUserService userService, JwtTokenService jwtTokenService, IEmailVerificationService emailVerificationService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _emailVerificationService = emailVerificationService;
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
            await _emailVerificationService.CreateAsync(new EmailVerificationToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = token,
                CreatedAt = DateTime.UtcNow
            }, cancellationToken);

            return token;
        }

        public async Task<bool> EmailConfirmationAsync(Guid userId, string token, CancellationToken cancellation)
        {
            var emailVerificationToken = await _emailVerificationService.GetByTokenAsync(token, cancellation);
            var user = await _userService.GetByIdAsync(userId, cancellation);

            if (emailVerificationToken is null || user.IsEmailConfirmed)
            {
                return false;
            }

            user.IsEmailConfirmed = true;
            await _userService.UpdateAsync(user, cancellation);
            await _emailVerificationService.DeleteByIdAsync(emailVerificationToken.Id, cancellation);
            return true;
        }
        public Task<string> LoginAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
