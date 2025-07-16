using System;

namespace UserService.Application.Dtos
{
    public record AuthResponseDto(
        bool Succeeded,
        string? Token,
        string? ErrorMessage);
}