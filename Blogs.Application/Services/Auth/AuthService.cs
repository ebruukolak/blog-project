using Blogs.Application.Exceptions;
using Blogs.Application.Helpers;
using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<bool> RegisterAsync(User user, CancellationToken cancellationToken)
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

            return isUserCreated;
        }
        public Task<string> LoginAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
