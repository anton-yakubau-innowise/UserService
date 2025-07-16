
namespace UserService.Application.Dtos;

public record RegisterRequestDto(
    string UserName,
    string Email,
    string Password,
    string FirstName,
    string LastName);
