using UserService.Application.Interfaces;
using UserService.Application.Dtos;
using UserService.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver.Linq;

namespace UserService.Application.Services
{
    public class UserApplicationService(UserManager<User> userManager, IMapper mapper) : IUserApplicationService
    {
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return mapper.Map<UserDto?>(user);
        }
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return mapper.Map<UserDto?>(user);
        }


        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = userManager.Users.ToList();
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        // public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        // {
        //     var users = userManager.Users.ToListAsync();
        //     return mapper.Map<IEnumerable<UserDto>>(users);
        // }

        public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
        {
            var user = User.RegisterNewUser(
                request.Email,
                request.UserName,
                request.FirstName,
                request.LastName
            );

            await userManager.CreateAsync(user);
            return mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            user.UpdateProfile(request.FirstName, request.LastName);
            await userManager.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            await userManager.DeleteAsync(user);
        }
    }
}