using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos;
using UserService.Application.Interfaces;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await authService.RegisterAsync(request);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(new LoginResponseDto(result.Token!));
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await authService.LoginAsync(request);
            if (!result.Succeeded)
            {
                return Unauthorized(result.ErrorMessage);
            }

            return Ok(new LoginResponseDto(result.Token!));
        }
    }
}
