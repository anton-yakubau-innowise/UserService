using UserService.Application.Interfaces;
using UserService.Application.Dtos;
using UserService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace UserService.Application.Services
{
    public class AuthService(UserManager<User> userManager, ITokenService tokenService) : IAuthService
    {

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var user = User.RegisterNewUser(
                request.Email,
                request.UserName,
                request.FirstName,
                request.LastName
            );

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new AuthResponseDto(false, null, result.Errors.FirstOrDefault()?.Description);

            var token = tokenService.GenerateJwtToken(user);
            if (string.IsNullOrEmpty(token))
                return new AuthResponseDto(false, null, "Failed to generate token.");

            return new AuthResponseDto(true, token, null);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return new AuthResponseDto(false, null, "Invalid username or password.");

            var isPasswordCorrect = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordCorrect)
                return new AuthResponseDto(false, null, "Invalid username or password.");

            var token = tokenService.GenerateJwtToken(user);
            if (string.IsNullOrEmpty(token))
                return new AuthResponseDto(false, null, "Failed to generate token.");

            return new AuthResponseDto(true, token, null);
        }
    }
}