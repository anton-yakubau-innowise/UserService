namespace UserService.Application.Dtos
{
    public record CreateUserRequest(
        string Email,
        string UserName,
        string FirstName,
        string LastName
    );
}