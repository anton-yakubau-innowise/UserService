namespace UserService.Application.Dtos
{
    public record LoginRequestDto(
        string UserName,
        string Password);

}