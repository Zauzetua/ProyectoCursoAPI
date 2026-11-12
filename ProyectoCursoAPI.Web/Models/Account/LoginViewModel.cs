using System.ComponentModel.DataAnnotations;

namespace ProyectoCursoAPI.Web.Models.Account
{
    // ViewModel para el formulario de inicio de sesion
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo electronico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electronico invalido.")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contrasena es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contrasena")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Recordarme en este equipo")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
