namespace ProyectoCursoAPI.Application.Features.Auth.DTOs
{
    // DTO para credenciales de acceso
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
