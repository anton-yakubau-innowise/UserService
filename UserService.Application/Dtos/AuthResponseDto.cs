namespace UserService.Application.Dtos
{
    public record AuthResponseDto(
        bool Succeeded,
        string? Token,
        UserDto? User,
        string? ErrorMessage);
}