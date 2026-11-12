using FluentValidation;

namespace ProyectoCursoAPI.Application.Features.Categorias.Validators
{
    // Validador independiente para UpdateCategoriaDto
    public class UpdateCategoriaDtoValidator : AbstractValidator<DTOs.UpdateCategoriaDto>
    {
        public UpdateCategoriaDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El ID de la categoria es obligatorio.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoria es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripcion no puede exceder 500 caracteres.");
        }
    }
}
