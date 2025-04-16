using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Users
{
    public interface IUserService
    {
        Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> UpdateAsync(User user, CancellationToken cancellationToken);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistByEmailAsync(string email, CancellationToken cancellationToken);

    }
}
