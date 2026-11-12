using FluentValidation;

namespace ProyectoCursoAPI.Application.Features.Productos.Validators
{
    // Validador de negocio para CreateProductoDto
    public class CreateProductoDtoValidator : AbstractValidator<DTOs.CreateProductoDto>
    {
        public CreateProductoDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripcion no puede exceder 500 caracteres.");

            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");

            RuleFor(x => x.IdCategoria)
                .NotEmpty().WithMessage("La categoria es obligatoria.");

            RuleFor(x => x.IdProveedor)
                .NotEmpty().WithMessage("El proveedor es obligatorio.");
        }
    }
}
