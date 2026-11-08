using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCursoAPI.Application.Common.Exceptions;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Models;
using ProyectoCursoAPI.Application.Common.Services.Categorias.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Obtiene todas las categorías activas (acceso público).
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CategoriaResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _categoriaService.GetAllAsync(cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Obtiene un listado paginado de categorías aplicando Skip y Take (acceso público).
        /// </summary>
        [HttpGet("paged")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResult<CategoriaResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationParams pagination, CancellationToken cancellationToken)
        {
            var response = await _categoriaService.GetPagedAsync(pagination, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Obtiene una categoría por su identificador único (acceso público).
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _categoriaService.GetByIdAsync(id, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Crea una nueva categoría con validación FluentValidation (requiere autenticación JWT).
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaDto dto, CancellationToken cancellationToken)
        {
            var response = await _categoriaService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Actualiza una categoría existente (requiere autenticación JWT).
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoriaDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                throw new ValidationException("Id", "El ID de la ruta no coincide con el ID del cuerpo de la petición.");
            }

            var response = await _categoriaService.UpdateAsync(dto, cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Elimina lógicamente una categoría por su identificador único (requiere autenticación JWT).
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _categoriaService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
