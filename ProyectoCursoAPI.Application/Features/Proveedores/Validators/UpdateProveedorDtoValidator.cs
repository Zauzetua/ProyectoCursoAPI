using FluentValidation;

namespace ProyectoCursoAPI.Application.Features.Proveedores.Validators
{
    // Validador de negocio para la actualizacion de proveedores
    public class UpdateProveedorDtoValidator : AbstractValidator<DTOs.UpdateProveedorDto>
    {
        public UpdateProveedorDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El ID del proveedor es obligatorio.");

            RuleFor(x => x.NombreComercial)
                .NotEmpty().WithMessage("El nombre comercial es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre comercial no puede exceder 150 caracteres.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La direccion es obligatoria.")
                .MaximumLength(250).WithMessage("La direccion no puede exceder 250 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electronico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electronico no es valido.")
                .MaximumLength(100).WithMessage("El correo electronico no puede exceder 100 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El telefono es obligatorio.")
                .Length(10, 15).WithMessage("El telefono debe tener entre 10 y 15 caracteres.");

            RuleFor(x => x.RFC)
                .NotEmpty().WithMessage("El RFC es obligatorio.")
                .Length(12, 13).WithMessage("El RFC debe contener entre 12 y 13 caracteres.");
        }
    }
}
