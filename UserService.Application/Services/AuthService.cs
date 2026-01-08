using UserService.Application.Interfaces;
using UserService.Application.Dtos;
using UserService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace UserService.Application.Services
{
    public class AuthService(UserManager<User> userManager, ITokenService tokenService, IMapper mapper) : IAuthService
    {

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var user = User.RegisterNewUser(
                request.Email,
                request.UserName,
                request.FirstName,
                request.LastName,
                request.PhoneNumber
            );

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join("\n", result.Errors.Select(e => e.Description));
                return new AuthResponseDto(false, null, null, errorMessage);
            }

            var token = tokenService.GenerateJwtToken(user);
            if (string.IsNullOrEmpty(token))
                return new AuthResponseDto(false, null, null, "Failed to generate token.");

            var userDto = mapper.Map<UserDto>(user);
            return new AuthResponseDto(true, token, userDto, null);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = request.LoginIdentifier.Contains("@")
                ? await userManager.FindByEmailAsync(request.LoginIdentifier)
                : await userManager.FindByNameAsync(request.LoginIdentifier);

            if (user == null)
                return new AuthResponseDto(false, null, null, "Invalid email or username.");

            var isPasswordCorrect = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordCorrect)
                return new AuthResponseDto(false, null, null, "Invalid password.");

            var token = tokenService.GenerateJwtToken(user);
            if (string.IsNullOrEmpty(token))
                return new AuthResponseDto(false, null, null, "Failed to generate token.");

            var userDto = mapper.Map<UserDto>(user);
            return new AuthResponseDto(true, token, userDto, null);
        }
    }
}