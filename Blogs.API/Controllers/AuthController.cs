using Blogs.API.Mapping;
using Blogs.Application.Services;
using Blogs.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
           _userService = userService;
        }

        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var user = registrationRequest.MaptoUser();

            

            return Ok();
        }
    }
}
