using Blogs.Application.Models;
using Blogs.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Roles
{
    public class RoleService : IRoleservice
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
                _roleRepository = roleRepository;
        }
        public async Task<bool> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            return await _roleRepository.CreateAsync(role, cancellationToken);
        }
           

        public Task<bool> ExistByNameAsync(string email, CancellationToken cancellationToken)
        {
            return _roleRepository.ExistByNameAsync(email, cancellationToken);
        }

        public Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
           return _roleRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return _roleRepository.GetByNameAsync(name, cancellationToken);
        }

        public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _roleRepository.DeleteByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await _roleRepository.GetAllAsync(cancellationToken);
        }
    }
}
