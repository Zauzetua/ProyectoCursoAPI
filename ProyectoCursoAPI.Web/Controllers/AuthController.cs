using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Services.Auth.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema y retorna un token JWT.
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(dto, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Inicia sesión con correo y contraseña para obtener un token JWT.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
        {
            var response = await _authService.LoginAsync(dto, cancellationToken);
            return Ok(response.Data);
        }
    }
}
