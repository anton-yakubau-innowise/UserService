
using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos;

public record RegisterRequestDto(
    [Required] string UserName,
    [Required] string Email,
    [Required] string Password,
    [Required] string FirstName,
    [Required] string LastName);
