using System.ComponentModel.DataAnnotations;

namespace ProyectoCursoAPI.Web.Models.Account
{
    // ViewModel para el formulario de registro de usuario
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El correo electronico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electronico invalido.")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contrasena es obligatoria.")]
        [StringLength(100, ErrorMessage = "La contrasena debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contrasena")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contrasena")]
        [Compare("Password", ErrorMessage = "Las contrasenas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
