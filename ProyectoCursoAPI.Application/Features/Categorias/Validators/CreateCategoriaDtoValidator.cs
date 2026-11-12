using FluentValidation;

namespace ProyectoCursoAPI.Application.Features.Categorias.Validators
{
    // Validador independiente para CreateCategoriaDto
    public class CreateCategoriaDtoValidator : AbstractValidator<DTOs.CreateCategoriaDto>
    {
        public CreateCategoriaDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoria es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripcion no puede exceder 500 caracteres.");
        }
    }
}
