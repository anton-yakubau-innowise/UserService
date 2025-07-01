namespace UserService.Application.Dtos
{
    public record UserDto(
        Guid Id,
        string Email,
        string Username,
        string FirstName,
        string LastName
    );
}