using FluentValidation;

namespace ProyectoCursoAPI.Application.Features.Auth.Validators
{
    // Validador para registro de nuevo usuario
    public class RegisterDtoValidator : AbstractValidator<DTOs.RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electronico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electronico no es valido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contrasena es obligatoria.")
                .MinimumLength(6).WithMessage("La contrasena debe tener al menos 6 caracteres.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirmar contrasena es obligatorio.")
                .Equal(x => x.Password).WithMessage("Las contrasenas no coinciden.");
        }
    }
}
