using Blogs.Application.Models;
using Blogs.Contracts.Requests;

namespace Blogs.API.Mapping
{
    public static class UserContractMapping
    {
        public static User MaptoUser(this RegistrationRequest registerationRequest)
        {
            var user = new User
            {
                Id = new Guid(),
                FirstName = registerationRequest.FirstName,
                LastName = registerationRequest.LastName,
                Email = registerationRequest.Email,
                Password = registerationRequest.Password,
                IsDeleted = false,
                RoleId = new Guid(), // TODO: will be handled
                CreatedAt = DateTime.UtcNow
            };

            return user;
        }
    }
}
