using System;
using Microsoft.AspNetCore.Mvc;
using ApiCrud.Models;
using ApiCrud.Services;
using ApiCrud.DTOs;

namespace ApiCrud.Controllers
{
    [ApiController]
    [Route("api/v1/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly LibroService _libroService;

        public LibrosController(LibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpPost]
        public IActionResult CrearLibro(LibroDTO libroDTO)
        {
            if (string.IsNullOrEmpty(libroDTO.Titulo) || string.IsNullOrEmpty(libroDTO.Resumen))
            {
                return BadRequest(new { error = "El título y el resumen son campos requeridos." });
            }

            var nuevoLibro = new Libro
            {
                Id = Guid.NewGuid(),
                Titulo = libroDTO.Titulo,
                Resumen = libroDTO.Resumen,
                AutorId = libroDTO.AutorId
            };

            _libroService.Crear(nuevoLibro);

            return Ok(new { message = "Libro creado exitosamente." });
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerLibroPorId(Guid id)
        {
            var libro = _libroService.ObtenerPorId(id);
            if (libro == null)
            {
                return NotFound(new { error = "No se encontró ningún libro con el ID especificado." });
            }
            return Ok(libro);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarLibro(Guid id, LibroDTO libroDTO)
        {
            if (string.IsNullOrEmpty(libroDTO.Titulo) || string.IsNullOrEmpty(libroDTO.Resumen))
            {
                return BadRequest(new { error = "El título y el resumen son campos requeridos." });
            }

            var libroExistente = _libroService.ObtenerPorId(id);

            if (libroExistente == null)
            {
                return NotFound(new { error = "No se encontró ningún libro con el ID especificado." });
            }

            libroExistente.Titulo = libroDTO.Titulo;
            libroExistente.Resumen = libroDTO.Resumen;

            _libroService.Actualizar(id, libroExistente);

            return Ok(new { message = "Libro actualizado exitosamente." });
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarLibro(Guid id)
        {
            var libro = _libroService.ObtenerPorId(id);
            if (libro == null)
            {
                return NotFound(new { error = "No se encontró ningún libro con el ID especificado." });
            }

            _libroService.Eliminar(id);

            return NoContent();
        }
    }
}
