using UserService.Application.Interfaces;
using UserService.Application.Dtos;
using UserService.Domain.Entities;
using AutoMapper;

namespace UserService.Application.Services
{
    public class UserApplicationService(IUnitOfWork unitOfWork, IMapper mapper) : IUserApplicationService
    {
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await unitOfWork.Users.GetByIdAsync(id);
            return mapper.Map<UserDto?>(user);
        }
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await unitOfWork.Users.GetByEmailAsync(email);
            return mapper.Map<UserDto?>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await unitOfWork.Users.ListAllAsync();
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
        {
            var user = User.RegisterNewUser(
                request.Email,
                request.UserName,
                request.FirstName,
                request.LastName
            );

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = await unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            user.UpdateProfile(request.FirstName, request.LastName);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await unitOfWork.Users.GetByIdAsync(id);
            if (user != null)
            {
                unitOfWork.Users.Delete(user);
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}