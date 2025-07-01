namespace UserService.Application.Dtos
{
    public record CreateUserRequest(
        string Email,
        string Username,
        string FirstName,
        string LastName
    );
}