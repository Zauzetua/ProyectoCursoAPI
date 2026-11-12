using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Categorias.DTOs;
using ProyectoCursoAPI.Domain.Exceptions;
using ProyectoCursoAPI.Web.Models.Categorias;
using ProyectoCursoAPI.Web.Models.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Controllers
{
    // Controlador MVC para Categorias: maneja la vista unica y endpoints AJAX para modales y tabla
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // 1. Vista principal unica
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var paged = await _categoriaService.GetPagedAsync(new PaginationParams(page, pageSize), cancellationToken);

            var model = new CategoriaIndexViewModel
            {
                PagedResult = paged,
                Pagination = new PaginationInfo
                {
                    CurrentPage = paged.PageNumber,
                    TotalPages = paged.TotalPages,
                    PageSize = paged.PageSize,
                    TotalCount = paged.TotalCount,
                    TargetAction = "GetTable"
                }
            };

            return View(model);
        }

        // 2. Carga asincrona del cuerpo de la tabla para paginado y refresco sin recargar la pagina
        [HttpGet]
        public async Task<IActionResult> GetTable(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var paged = await _categoriaService.GetPagedAsync(new PaginationParams(page, pageSize), cancellationToken);

            var model = new CategoriaIndexViewModel
            {
                PagedResult = paged,
                Pagination = new PaginationInfo
                {
                    CurrentPage = paged.PageNumber,
                    TotalPages = paged.TotalPages,
                    PageSize = paged.PageSize,
                    TotalCount = paged.TotalCount,
                    TargetAction = "GetTable"
                }
            };

            return PartialView("_TablePartial", model);
        }

        // 3. Obtener modal de creacion (HTML parcial)
        [HttpGet]
        public IActionResult GetCreateModal()
        {
            var form = new CategoriaFormViewModel();
            return PartialView("_FormModal", form);
        }

        // 4. Procesar creacion via AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaFormViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_FormModal", model);
            }

            try
            {
                var dto = new CreateCategoriaDto
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion ?? string.Empty
                };

                await _categoriaService.CreateAsync(dto, cancellationToken);
                return Json(new { success = true, message = "Categoria creada exitosamente." });
            }
            catch (ValidationException ex)
            {
                foreach (var err in ex.Errors)
                {
                    foreach (var msg in err.Value)
                    {
                        ModelState.AddModelError(err.Key, msg);
                    }
                }
                return PartialView("_FormModal", model);
            }
            catch (DomainException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView("_FormModal", model);
            }
        }

        // 5. Obtener modal de edicion con datos cargados
        [HttpGet]
        public async Task<IActionResult> GetEditModal(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var cat = await _categoriaService.GetByIdAsync(id, cancellationToken);
                var form = new CategoriaFormViewModel
                {
                    Id = cat.Id,
                    Nombre = cat.Nombre,
                    Descripcion = cat.Descripcion
                };
                return PartialView("_FormModal", form);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // 6. Procesar edicion via AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoriaFormViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || !model.Id.HasValue)
            {
                return PartialView("_FormModal", model);
            }

            try
            {
                var dto = new UpdateCategoriaDto
                {
                    Id = model.Id.Value,
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion ?? string.Empty,
                    Activo = true
                };

                await _categoriaService.UpdateAsync(dto, cancellationToken);
                return Json(new { success = true, message = "Categoria actualizada exitosamente." });
            }
            catch (ValidationException ex)
            {
                foreach (var err in ex.Errors)
                {
                    foreach (var msg in err.Value)
                    {
                        ModelState.AddModelError(err.Key, msg);
                    }
                }
                return PartialView("_FormModal", model);
            }
            catch (DomainException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView("_FormModal", model);
            }
        }

        // 7. Eliminacion logica via AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _categoriaService.DeleteAsync(id, cancellationToken);
                return Json(new { success = true, message = "Categoria eliminada exitosamente." });
            }
            catch (NotFoundException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
