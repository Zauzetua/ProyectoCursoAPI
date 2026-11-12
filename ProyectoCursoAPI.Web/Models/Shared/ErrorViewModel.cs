namespace ProyectoCursoAPI.Web.Models.Shared
{
    // Modelo de datos para la vista de error
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string? Message { get; set; }
    }
}
