using System;
using Microsoft.AspNetCore.Mvc;
using ApiCrud.Models;
using ApiCrud.Services;
using ApiCrud.DTOs;

namespace ApiCrud.Controllers
{
    [ApiController]
    [Route("api/v1/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly AutorService _autorService;

        public AutoresController(AutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpPost]
        public IActionResult CrearAutor(AutorDTO autorDTO)
        {
            if (string.IsNullOrEmpty(autorDTO.Nombre) || string.IsNullOrEmpty(autorDTO.Nacionalidad))
            {
                return BadRequest(new { error = "El nombre y la nacionalidad del autor son campos requeridos." });
            }

            var nuevoAutor = new Autor
            {
                Id = Guid.NewGuid(),
                Nombre = autorDTO.Nombre,
                Nacionalidad = autorDTO.Nacionalidad
            };

            _autorService.Crear(nuevoAutor);

            return Ok(new { message = "Autor creado exitosamente." });
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerAutorPorId(Guid id)
        {
            var autor = _autorService.ObtenerPorId(id);
            if (autor == null)
            {
                return NotFound(new { error = "No se encontró ningún autor con el ID especificado." });
            }
            return Ok(autor);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarAutor(Guid id, AutorDTO autorDTO)
        {
            if (string.IsNullOrEmpty(autorDTO.Nombre) || string.IsNullOrEmpty(autorDTO.Nacionalidad))
            {
                return BadRequest(new { error = "El nombre y la nacionalidad del autor son campos requeridos." });
            }

            var autorExistente = _autorService.ObtenerPorId(id);

            if (autorExistente == null)
            {
                return NotFound(new { error = "No se encontró ningún autor con el ID especificado." });
            }

            autorExistente.Nombre = autorDTO.Nombre;
            autorExistente.Nacionalidad = autorDTO.Nacionalidad;

            _autorService.Actualizar(id, autorExistente);

            return Ok(new { message = "Autor actualizado exitosamente." });
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarAutor(Guid id)
        {
            var autor = _autorService.ObtenerPorId(id);
            if (autor == null)
            {
                return NotFound(new { error = "No se encontró ningún autor con el ID especificado." });
            }

            _autorService.Eliminar(id);

            return NoContent();
        }
    }
}
