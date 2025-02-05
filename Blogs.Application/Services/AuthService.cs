using Blogs.Application.Exceptions;
using Blogs.Application.Helpers;
using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<string> RegisterAsync(User user, CancellationToken cancellationToken)
        {
            var userExist = await _userService.ExistByEmailAsync(user.Email, cancellationToken);

            if (userExist)
            {
                throw new UserAlreadyExistsException(user.Email);
            }


            var hashedPassword = PasswordHelper.HashPassword(user.Password);

            user.Password = hashedPassword;
            await _userService.CreateAsync(user,cancellationToken);


            return null;
        }
        public Task<string> LoginAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
