using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoCursoAPI.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string Token, DateTime Expiration) GenerateToken(string userId, string userName, string email, IList<string> roles)
        {
            var keyString = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("La clave secreta 'Jwt:Key' no está configurada.");
            var issuer = _configuration["Jwt:Issuer"] ?? "ProyectoCursoAPI";
            var audience = _configuration["Jwt:Audience"] ?? "ProyectoCursoAPI";
            var durationInMinutes = int.TryParse(_configuration["Jwt:DurationInMinutes"], out var minutes) ? minutes : 60;

            var expiration = DateTime.UtcNow.AddMinutes(durationInMinutes);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, userName),
                new(JwtRegisteredClaimNames.Email, email),
                new(ClaimTypes.Email, email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return (token, expiration);
        }
    }
}
