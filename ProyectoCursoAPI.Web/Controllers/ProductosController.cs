using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Features.Productos.DTOs;
using ProyectoCursoAPI.Domain.Exceptions;
using ProyectoCursoAPI.Web.Models.Productos;
using ProyectoCursoAPI.Web.Models.Shared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Controllers
{
    // Controlador MVC para Productos: maneja la vista unica, dropdowns y endpoints AJAX
    [Authorize]
    public class ProductosController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IProveedorService _proveedorService;

        public ProductosController(
            IProductoService productoService,
            ICategoriaService categoriaService,
            IProveedorService proveedorService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
            _proveedorService = proveedorService;
        }

        // Carga de opciones para selects en modales
        private async Task CargarDropdownsAsync(ProductoFormViewModel model, CancellationToken cancellationToken)
        {
            var categorias = await _categoriaService.GetAllAsync(cancellationToken);
            var proveedores = await _proveedorService.GetAllAsync(cancellationToken);

            model.CategoriasList = categorias.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == model.IdCategoria
            });

            model.ProveedoresList = proveedores.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.NombreComercial,
                Selected = p.Id == model.IdProveedor
            });
        }

        // 1. Vista principal unica
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var paged = await _productoService.GetPagedAsync(new PaginationParams(page, pageSize), cancellationToken);

            var model = new ProductoIndexViewModel
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

        // 2. Carga asincrona del cuerpo de la tabla para paginado y refresco sin recargar
        [HttpGet]
        public async Task<IActionResult> GetTable(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var paged = await _productoService.GetPagedAsync(new PaginationParams(page, pageSize), cancellationToken);

            var model = new ProductoIndexViewModel
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

        // 3. Obtener modal de creacion con selects poblados
        [HttpGet]
        public async Task<IActionResult> GetCreateModal(CancellationToken cancellationToken)
        {
            var form = new ProductoFormViewModel();
            await CargarDropdownsAsync(form, cancellationToken);
            return PartialView("_FormModal", form);
        }

        // 4. Procesar creacion via AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoFormViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await CargarDropdownsAsync(model, cancellationToken);
                return PartialView("_FormModal", model);
            }

            try
            {
                var dto = new CreateProductoDto
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion ?? string.Empty,
                    Precio = model.Precio,
                    Stock = model.Stock,
                    IdCategoria = model.IdCategoria,
                    IdProveedor = model.IdProveedor
                };

                await _productoService.CreateAsync(dto, cancellationToken);
                return Json(new { success = true, message = "Producto creado exitosamente." });
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
                await CargarDropdownsAsync(model, cancellationToken);
                return PartialView("_FormModal", model);
            }
            catch (DomainException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await CargarDropdownsAsync(model, cancellationToken);
                return PartialView("_FormModal", model);
            }
        }

        // 5. Obtener modal de edicion con datos y dropdowns
        [HttpGet]
        public async Task<IActionResult> GetEditModal(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var prod = await _productoService.GetByIdAsync(id, cancellationToken);
                var form = new ProductoFormViewModel
                {
                    Id = prod.Id,
                    Nombre = prod.Nombre,
                    Descripcion = prod.Descripcion,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    IdCategoria = prod.IdCategoria,
                    IdProveedor = prod.IdProveedor
                };

                await CargarDropdownsAsync(form, cancellationToken);
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
        public async Task<IActionResult> Edit(ProductoFormViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || !model.Id.HasValue)
            {
                await CargarDropdownsAsync(model, cancellationToken);
                return PartialView("_FormModal", model);
            }

            try
            {
                var dto = new UpdateProductoDto
                {
                    Id = model.Id.Value,
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion ?? string.Empty,
                    Precio = model.Precio,
                    Stock = model.Stock,
                    IdCategoria = model.IdCategoria,
                    IdProveedor = model.IdProveedor,
                    Activo = true
                };

                await _productoService.UpdateAsync(dto, cancellationToken);
                return Json(new { success = true, message = "Producto actualizado exitosamente." });
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
                await CargarDropdownsAsync(model, cancellationToken);
                return PartialView("_FormModal", model);
            }
            catch (DomainException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await CargarDropdownsAsync(model, cancellationToken);
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
                await _productoService.DeleteAsync(id, cancellationToken);
                return Json(new { success = true, message = "Producto eliminado exitosamente." });
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
