using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> CreateAsync(Role role, CancellationToken cancellationToken);
        Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistByNameAsync(string email, CancellationToken cancellationToken);
    }
}
