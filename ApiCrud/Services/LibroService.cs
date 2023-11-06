using System;
using System.Collections.Generic;
using System.Linq;
using ApiCrud.Models;

namespace ApiCrud.Services
{
    public class LibroService
    {
        private readonly List<Libro> _libros;

        public LibroService()
        {
            _libros = new List<Libro>();
        }

        public List<Libro> ObtenerTodos()
        {
            return _libros;
        }

        public Libro ObtenerPorId(Guid id)
        {
            return _libros.FirstOrDefault(l => l.Id == id);
        }

        public void Crear(Libro libro)
        {
            _libros.Add(libro);
        }

        public void Actualizar(Guid id, Libro libroActualizado)
        {
            var index = _libros.FindIndex(l => l.Id == id);
            if (index != -1)
            {
                _libros[index] = libroActualizado;
            }
        }

        public void Eliminar(Guid id)
        {
            var libro = _libros.FirstOrDefault(l => l.Id == id);
            if (libro != null)
            {
                _libros.Remove(libro);
            }
        }
    }
}
