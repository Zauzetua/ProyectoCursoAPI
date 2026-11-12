using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCursoAPI.Web.Models.Proveedores
{
    // ViewModel para el formulario en modal (Crear y Editar Proveedor)
    public class ProveedorFormViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "El nombre comercial es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre comercial no puede exceder 150 caracteres.")]
        [Display(Name = "Nombre Comercial")]
        public string NombreComercial { get; set; } = string.Empty;

        [Required(ErrorMessage = "La direccion es obligatoria.")]
        [StringLength(250, ErrorMessage = "La direccion no puede exceder 250 caracteres.")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El telefono es obligatorio.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "El telefono debe tener entre 10 y 15 caracteres.")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RFC es obligatorio.")]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "El RFC debe tener entre 12 y 13 caracteres.")]
        [Display(Name = "RFC")]
        public string RFC { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electronico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electronico invalido.")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; } = string.Empty;

        public bool EsEdicion => Id.HasValue && Id.Value != Guid.Empty;
    }
}
