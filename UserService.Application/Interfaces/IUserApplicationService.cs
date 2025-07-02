using UserService.Application.Dtos;

namespace UserService.Application.Interfaces
{
    public interface IUserApplicationService
    {
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(CreateUserRequest createRequest);
        Task DeleteUserAsync(Guid id);
        Task UpdateUserAsync(Guid id, UpdateUserRequest updateRequest);
    }
}