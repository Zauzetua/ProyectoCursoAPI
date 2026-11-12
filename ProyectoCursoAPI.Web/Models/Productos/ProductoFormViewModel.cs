using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCursoAPI.Web.Models.Productos
{
    // ViewModel para el formulario en modal (Crear y Editar Producto con Dropdowns)
    public class ProductoFormViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        [Display(Name = "Nombre del Producto")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripcion no puede exceder 500 caracteres.")]
        [Display(Name = "Descripcion")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0.")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, 100000, ErrorMessage = "El stock no puede ser negativo.")]
        [Display(Name = "Stock Disponible")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Seleccione una categoria.")]
        [Display(Name = "Categoria")]
        public Guid IdCategoria { get; set; }

        [Required(ErrorMessage = "Seleccione un proveedor.")]
        [Display(Name = "Proveedor")]
        public Guid IdProveedor { get; set; }

        public bool EsEdicion => Id.HasValue && Id.Value != Guid.Empty;

        // Listas desplegables para la vista modal
        public IEnumerable<SelectListItem> CategoriasList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ProveedoresList { get; set; } = new List<SelectListItem>();
    }
}
