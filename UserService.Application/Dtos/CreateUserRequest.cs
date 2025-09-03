using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos
{
    public record CreateUserRequest(
        [Required] string Email,
        [Required] string UserName,
        [Required] string Password,
        [Required] string FirstName,
        [Required] string LastName
    );
}