using FluentValidation;

namespace ProyectoCursoAPI.Application.Features.Auth.Validators
{
    // Validador de inicio de sesion
    public class LoginDtoValidator : AbstractValidator<DTOs.LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electronico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electronico no es valido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contrasena es obligatoria.");
        }
    }
}
