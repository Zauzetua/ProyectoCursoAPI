using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Features.Auth.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Infrastructure.Services
{
    // Servicio de autenticacion utilizando SignInManager y UserManager para sesiones web (Cookies)
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _registerValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser is not null)
            {
                throw new ValidationException("Email", "Ya existe un usuario registrado con este correo electronico.");
            }

            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded)
            {
                var errorMessages = string.Join(" ", createResult.Errors.Select(e => e.Description));
                throw new ValidationException("Password", errorMessages);
            }

            // Inicia sesion automaticamente al registrarse
            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }

        public async Task<bool> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _loginValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
            {
                throw new ValidationException("Credenciales", "El correo electronico o la contrasena son incorrectos.");
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!,
                dto.Password,
                isPersistent: dto.RememberMe,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new ValidationException("Credenciales", "El correo electronico o la contrasena son incorrectos.");
            }

            return true;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
