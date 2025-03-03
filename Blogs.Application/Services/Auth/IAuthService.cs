using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Auth
{
    public interface IAuthService
    {
        public Task<string> RegisterAsync(User user, CancellationToken cancellation);
        public Task<bool> EmailConfirmationAsync(Guid userId,string token, CancellationToken cancellation);
        public Task<string> LoginAsync(User user, CancellationToken cancellation);

    }
}
