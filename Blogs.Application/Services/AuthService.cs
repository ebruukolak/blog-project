using Blogs.Application.Exceptions;
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
        public async Task<string> RegisterAsync(User user, CancellationToken cancellationToke)
        {
            var userExist = await _userService.ExistByEmailAsync(user.Email, cancellationToke);

            if (userExist)
            {
                throw new UserAlreadyExistsException(user.Email);
            }




            return null;
        }
        public Task<string> LoginAsync(User user, CancellationToken cancellationToke)
        {
            throw new NotImplementedException();
        }


    }
}
