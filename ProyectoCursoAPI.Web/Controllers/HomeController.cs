using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursoAPI.Web.Models.Shared;
using System.Diagnostics;

namespace ProyectoCursoAPI.Web.Controllers
{
    // Controlador principal de inicio y manejo de errores genericos
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Productos");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = "Ocurrio un error inesperado al procesar la solicitud."
            });
        }

        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}
