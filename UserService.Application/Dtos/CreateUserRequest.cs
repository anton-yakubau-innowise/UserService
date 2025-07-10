namespace UserService.Application.Dtos
{
    public record CreateUserRequest(
        string Email,
        string UserName,
        string Password,
        string FirstName,
        string LastName
    );
}