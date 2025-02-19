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
                Id = Guid.NewGuid(),                
                FirstName = registerationRequest.FirstName,
                LastName = registerationRequest.LastName,
                Email = registerationRequest.Email,
                Password = registerationRequest.Password,
                IsDeleted = false,
                RoleId = new Guid("FD471ED1-A021-49C8-9436-BC27E427CD6F"),//Guid.NewGuid(), // TODO: will be handled
                CreatedAt = DateTime.UtcNow
            };

            return user;
        }
    }
}
