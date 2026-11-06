using FluentValidation;
using System;

namespace ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs
{
    public record UpdateProveedorDto(
        Guid Id,
        string NombreComercial,
        string Direccion,
        string Telefono,
        string RFC,
        string Email
    );

    public class UpdateProveedorValidator : AbstractValidator<UpdateProveedorDto>
    {
        public UpdateProveedorValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador del proveedor es obligatorio.");

            RuleFor(x => x.NombreComercial)
                .NotEmpty().WithMessage("El nombre comercial del proveedor es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre comercial no puede exceder los 100 caracteres.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(200).WithMessage("La dirección no puede exceder los 200 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .Length(10, 15).WithMessage("El teléfono debe tener entre 10 y 15 dígitos.");

            RuleFor(x => x.RFC)
                .NotEmpty().WithMessage("El RFC es obligatorio.")
                .Length(12, 13).WithMessage("El RFC debe contener entre 12 y 13 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electrónico es inválido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder los 100 caracteres.");
        }
    }
}
