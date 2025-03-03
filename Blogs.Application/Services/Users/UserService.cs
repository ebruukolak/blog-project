using Blogs.Application.Helpers;
using Blogs.Application.Models;
using Blogs.Application.Repositories;

namespace Blogs.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
        {
            //TODO: Return back for role operations
            user.PasswordHash = PasswordHelper.HashPassword(user.Password);
            return _userRepository.CreateAsync(user, cancellationToken);
        }

        public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _userRepository.GetAllAsync(cancellationToken);
        }

        public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            //TODO: Return back for role operations
            var userExist = await _userRepository.ExistByIdAsync(user.Id, cancellationToken);
            if (!userExist)
            {
                return null;
            }
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user, cancellationToken);
            return user;
        }

        public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.DeleteByIdAsync(id, cancellationToken);
        }

        public Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.ExistByIdAsync(id, cancellationToken);
        }
        public Task<bool> ExistByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _userRepository.ExistByEmailAsync(email, cancellationToken);
        }
    }
}
