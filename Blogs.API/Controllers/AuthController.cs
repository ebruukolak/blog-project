using Blogs.API.Mapping;
using Blogs.Application.Services.Auth;
using Blogs.Application.Services.Email;
using Blogs.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }


        [HttpPost(ApiEndpoints.Auth.Register)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest, CancellationToken cancellationToken)
        {
            var user = registrationRequest.MaptoUser();

            var generatedToken = await _authService.RegisterAsync(user, cancellationToken);
            if (generatedToken is null)
                return BadRequest();

            await _emailService.SendEmailAsync(user, generatedToken, ApiEndpoints.Auth.EmailConfirmation, cancellationToken);

            return Ok("Registration successful. Please check your email to confirm.");
        }

        [HttpGet(ApiEndpoints.Auth.EmailConfirmation)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] Guid userId, [FromQuery] string token,CancellationToken cancellationToken)
        {
            var isConfirmed = await _authService.EmailConfirmationAsync(userId, token, cancellationToken);

            if(!isConfirmed)
                return BadRequest("Verification token expired");

            return Ok();
        }
        [HttpPost(ApiEndpoints.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest,CancellationToken cancellationToken)
        {
           var user = loginRequest.MaptoUser();
            var token = await _authService.LoginAsync(user, cancellationToken);
            return Ok(new { Token = token});
        }
    } 
}
