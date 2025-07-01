namespace UserService.Application.Dtos
{
    public record UpdateUserRequest(
        string? Email = null,
        string? Username = null,
        string? FirstName = null,
        string? LastName = null
    );
}