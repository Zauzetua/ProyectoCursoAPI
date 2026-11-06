using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Productos.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>
        /// Obtiene todos los productos activos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductoResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _productoService.GetAllAsync(cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Obtiene un listado paginado de productos aplicando Skip y Take.
        /// </summary>
        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResult<ProductoResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationParams pagination, CancellationToken cancellationToken)
        {
            var response = await _productoService.GetPagedAsync(pagination, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Obtiene un producto por su identificador único.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _productoService.GetByIdAsync(id, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Crea un nuevo producto con validación FluentValidation.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateProductoDto dto, CancellationToken cancellationToken)
        {
            var response = await _productoService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProductoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductoDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                throw new ValidationException("Id", "El ID de la ruta no coincide con el ID del cuerpo de la petición.");
            }

            var response = await _productoService.UpdateAsync(dto, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Elimina lógicamente un producto por su identificador único.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _productoService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
