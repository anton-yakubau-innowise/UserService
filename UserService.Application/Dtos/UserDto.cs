namespace UserService.Application.Dtos
{
    public record UserDto(
        Guid Id,
        string Email,
        string UserName,
        string FirstName,
        string LastName
    );
}