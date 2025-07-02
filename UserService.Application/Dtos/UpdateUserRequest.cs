namespace UserService.Application.Dtos
{
    public record UpdateUserRequest(
        string? Email = null,
        string? UserName = null,
        string? FirstName = null,
        string? LastName = null
    );
}