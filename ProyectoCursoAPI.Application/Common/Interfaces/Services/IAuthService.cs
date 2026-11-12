using ProyectoCursoAPI.Application.Features.Auth.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    // Contrato para autenticacion y gestion de sesiones
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
        Task<bool> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
        Task LogoutAsync();
    }
}
