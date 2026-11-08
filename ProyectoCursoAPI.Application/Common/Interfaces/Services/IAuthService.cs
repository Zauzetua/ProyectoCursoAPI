using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Auth.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<AuthResponseDto>> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
        Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    }
}
