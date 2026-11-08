using System;
using System.Collections.Generic;

namespace ProyectoCursoAPI.Application.Common.Services.Auth.DTOs
{
    public record AuthResponseDto
    {
        public string Token { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public DateTime Expiration { get; init; }
        public IList<string> Roles { get; init; } = [];
    }
}
