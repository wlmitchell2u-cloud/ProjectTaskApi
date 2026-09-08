using EnterpriseTaskManager.Application.DTOs.Auth;
using EnterpriseTaskManager.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<AuthResponseDto> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(dto, cancellationToken);
            return (result);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AuthResponseDto> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(dto.Email, dto.Password, cancellationToken);
            return result;
        }
    }
}
