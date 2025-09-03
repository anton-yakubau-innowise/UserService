using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos
{
    public record LoginRequestDto(
        [Required] string UserName,
        [Required] string Password);

}