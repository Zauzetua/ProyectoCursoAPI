using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Proveedores.DTOs;
using ProyectoCursoAPI.Domain.Exceptions;
using ProyectoCursoAPI.Web.Models.Proveedores;
using ProyectoCursoAPI.Web.Models.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Controllers
{
    // Controlador MVC para Proveedores: maneja la vista unica y endpoints AJAX para modales y tabla
    [Authorize]
    public class ProveedoresController : Controller
    {
        private readonly IProveedorService _proveedorService;

        public ProveedoresController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        // 1. Vista principal unica
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var paged = await _proveedorService.GetPagedAsync(new PaginationParams(page, pageSize), cancellationToken);

            var model = new ProveedorIndexViewModel
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
            var paged = await _proveedorService.GetPagedAsync(new PaginationParams(page, pageSize), cancellationToken);

            var model = new ProveedorIndexViewModel
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
            var form = new ProveedorFormViewModel();
            return PartialView("_FormModal", form);
        }

        // 4. Procesar creacion via AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProveedorFormViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_FormModal", model);
            }

            try
            {
                var dto = new CreateProveedorDto
                {
                    NombreComercial = model.NombreComercial,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    RFC = model.RFC,
                    Email = model.Email
                };

                await _proveedorService.CreateAsync(dto, cancellationToken);
                return Json(new { success = true, message = "Proveedor creado exitosamente." });
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
                var prov = await _proveedorService.GetByIdAsync(id, cancellationToken);
                var form = new ProveedorFormViewModel
                {
                    Id = prov.Id,
                    NombreComercial = prov.NombreComercial,
                    Direccion = prov.Direccion,
                    Telefono = prov.Telefono,
                    RFC = prov.RFC,
                    Email = prov.Email
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
        public async Task<IActionResult> Edit(ProveedorFormViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || !model.Id.HasValue)
            {
                return PartialView("_FormModal", model);
            }

            try
            {
                var dto = new UpdateProveedorDto
                {
                    Id = model.Id.Value,
                    NombreComercial = model.NombreComercial,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    RFC = model.RFC,
                    Email = model.Email,
                    Activo = true
                };

                await _proveedorService.UpdateAsync(dto, cancellationToken);
                return Json(new { success = true, message = "Proveedor actualizado exitosamente." });
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
                await _proveedorService.DeleteAsync(id, cancellationToken);
                return Json(new { success = true, message = "Proveedor eliminado exitosamente." });
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
