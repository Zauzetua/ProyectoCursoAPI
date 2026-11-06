using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Proveedores.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedoresController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        /// <summary>
        /// Obtiene todos los proveedores activos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProveedorResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _proveedorService.GetAllAsync(cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Obtiene un listado paginado de proveedores aplicando Skip y Take.
        /// </summary>
        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResult<ProveedorResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationParams pagination, CancellationToken cancellationToken)
        {
            var response = await _proveedorService.GetPagedAsync(pagination, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Obtiene un proveedor por su identificador único.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProveedorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _proveedorService.GetByIdAsync(id, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Obtiene un proveedor por su RFC.
        /// </summary>
        [HttpGet("rfc/{rfc}")]
        [ProducesResponseType(typeof(ProveedorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByRfc(string rfc, CancellationToken cancellationToken)
        {
            var response = await _proveedorService.GetByRfcAsync(rfc, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Crea un nuevo proveedor con validación FluentValidation.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProveedorResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProveedorDto dto, CancellationToken cancellationToken)
        {
            var response = await _proveedorService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Actualiza un proveedor existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProveedorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProveedorDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                throw new ValidationException("Id", "El ID de la ruta no coincide con el ID del cuerpo de la petición.");
            }

            var response = await _proveedorService.UpdateAsync(dto, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Elimina lógicamente un proveedor por su identificador único.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _proveedorService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
