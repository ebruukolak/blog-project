using Blogs.API.Mapping;
using Blogs.Application.Services;
using Blogs.Application.Services.Auth;
using Blogs.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
           _authService = authService;
        }


        [HttpPost(ApiEndpoints.Auth.Register)]  
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest,CancellationToken cancellationToken)
        {
            var user = registrationRequest.MaptoUser();

            var isRegistered = await _authService.RegisterAsync(user, cancellationToken);
            if (!isRegistered)
                return BadRequest();
            
            return Created();
        }
    }
}
