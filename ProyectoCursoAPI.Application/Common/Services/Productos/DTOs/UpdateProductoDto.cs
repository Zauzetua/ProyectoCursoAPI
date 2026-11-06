using FluentValidation;
using System;

namespace ProyectoCursoAPI.Application.Common.Services.Productos.DTOs
{
    public record UpdateProductoDto(
        Guid Id,
        string Nombre,
        string Descripcion,
        decimal Precio,
        int Stock,
        Guid IdCategoria,
        Guid IdProveedor
    );

    public class UpdateProductoValidator : AbstractValidator<UpdateProductoDto>
    {
        public UpdateProductoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador del producto es obligatorio.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción del producto es obligatoria.")
                .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres.");

            RuleFor(x => x.Precio)
                .GreaterThanOrEqualTo(0).WithMessage("El precio del producto no puede ser negativo.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock del producto no puede ser negativo.");

            RuleFor(x => x.IdCategoria)
                .NotEmpty().WithMessage("La categoría es obligatoria.");

            RuleFor(x => x.IdProveedor)
                .NotEmpty().WithMessage("El proveedor es obligatorio.");
        }
    }
}
