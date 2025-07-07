using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService) : ControllerBase
    {

        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = Domain.Entities.User.RegisterNewUser(
                request.Email,
                request.UserName,
                request.FirstName,
                request.LastName
            );
            var result = await userManager.CreateAsync(user, request.Password);
            
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var token = tokenService.GenerateJwtToken(user);
            return Ok(new LoginResponseDto(token));
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.Succeeded)
                return Unauthorized("Invalid credentials");

            var token = tokenService.GenerateJwtToken(user);
            return Ok(new LoginResponseDto(token));
        }
    }
}
