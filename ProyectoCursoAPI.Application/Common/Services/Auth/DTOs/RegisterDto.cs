using FluentValidation;

namespace ProyectoCursoAPI.Application.Common.Services.Auth.DTOs
{
    public record RegisterDto(string Email, string Password, string ConfirmPassword);

    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electrónico es inválido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder los 100 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("La confirmación de contraseña es obligatoria.")
                .Equal(x => x.Password).WithMessage("Las contraseñas no coinciden.");
        }
    }
}
