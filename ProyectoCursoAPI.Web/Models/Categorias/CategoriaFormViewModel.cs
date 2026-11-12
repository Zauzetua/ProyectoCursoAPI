using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCursoAPI.Web.Models.Categorias
{
    // ViewModel para el formulario en modal (Crear y Editar)
    public class CategoriaFormViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        [Display(Name = "Nombre de la Categoria")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripcion no puede exceder 500 caracteres.")]
        [Display(Name = "Descripcion")]
        public string? Descripcion { get; set; }

        public bool EsEdicion => Id.HasValue && Id.Value != Guid.Empty;
    }
}
