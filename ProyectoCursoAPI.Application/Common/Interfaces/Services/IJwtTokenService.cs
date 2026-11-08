using System;
using System.Collections.Generic;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    public interface IJwtTokenService
    {
        (string Token, DateTime Expiration) GenerateToken(string userId, string userName, string email, IList<string> roles);
    }
}
