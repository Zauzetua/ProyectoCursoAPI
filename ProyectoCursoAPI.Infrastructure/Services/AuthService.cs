using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Auth.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = ProyectoCursoAPI.Application.Common.Exceptions.ValidationException;

namespace ProyectoCursoAPI.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;

        public AuthService(
            UserManager<IdentityUser> userManager,
            IJwtTokenService jwtTokenService,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        public async Task<ServiceResponse<AuthResponseDto>> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _registerValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser is not null)
            {
                throw new ValidationException("Email", "Ya existe un usuario registrado con este correo electrónico.");
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

            var roles = new List<string> { "Usuario" };

            var (token, expiration) = _jwtTokenService.GenerateToken(user.Id, user.UserName!, user.Email!, roles);

            var response = new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                Expiration = expiration,
                Roles = roles
            };

            return ServiceResponse<AuthResponseDto>.Ok(response, "Usuario registrado exitosamente.");
        }

        public async Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _loginValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
            {
                throw new ValidationException("Credenciales", "El correo electrónico o la contraseña son incorrectos.");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!checkPassword)
            {
                throw new ValidationException("Credenciales", "El correo electrónico o la contraseña son incorrectos.");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesList = userRoles.Count > 0 ? userRoles : new List<string> { "Usuario" };

            var (token, expiration) = _jwtTokenService.GenerateToken(user.Id, user.UserName!, user.Email!, rolesList);

            var response = new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                Expiration = expiration,
                Roles = rolesList
            };

            return ServiceResponse<AuthResponseDto>.Ok(response, "Inicio de sesión exitoso.");
        }
    }
}
