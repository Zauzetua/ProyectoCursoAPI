using Microsoft.AspNetCore.Mvc;
using ProyectoCursoAPI.Application.Common.Interfaces.Services;
using ProyectoCursoAPI.Application.Common.Services.Categorias.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoCursoAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Obtiene todas las categorías activas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _categoriaService.GetAllAsync(cancellationToken);
            return Ok(response.Data);
        }

        /// <summary>
        /// Obtiene una categoría por su identificador único.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _categoriaService.GetByIdAsync(id, cancellationToken);
            if (!response.Success)
            {
                return NotFound(new { mensaje = response.Message });
            }

            return Ok(response.Data);
        }

        /// <summary>
        /// Crea una nueva categoría.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaDto dto, CancellationToken cancellationToken)
        {
            var response = await _categoriaService.CreateAsync(dto, cancellationToken);
            if (!response.Success)
            {
                return BadRequest(new { mensaje = response.Message });
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoriaDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest(new { mensaje = "El ID de la ruta no coincide con el ID del cuerpo de la petición." });
            }

            var response = await _categoriaService.UpdateAsync(dto, cancellationToken);
            if (!response.Success)
            {
                return NotFound(new { mensaje = response.Message });
            }

            return Ok(response.Data);
        }

        /// <summary>
        /// Elimina lógicamente una categoría por su identificador único.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var response = await _categoriaService.DeleteAsync(id, cancellationToken);
            if (!response.Success)
            {
                return NotFound(new { mensaje = response.Message });
            }

            return NoContent();
        }
    }
}
