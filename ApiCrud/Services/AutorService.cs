using System;
using System.Collections.Generic;
using System.Linq;
using ApiCrud.Models;

namespace ApiCrud.Services
{
    public class AutorService
    {
        private readonly List<Autor> _autores;

        public AutorService()
        {
            _autores = new List<Autor>();
        }

        public List<Autor> ObtenerTodos()
        {
            return _autores;
        }

        public Autor ObtenerPorId(Guid id)
        {
            return _autores.FirstOrDefault(a => a.Id == id);
        }

        public void Crear(Autor autor)
        {
            _autores.Add(autor);
        }

        public void Actualizar(Guid id, Autor autorActualizado)
        {
            var index = _autores.FindIndex(a => a.Id == id);
            if (index != -1)
            {
                _autores[index] = autorActualizado;
            }
        }

        public void Eliminar(Guid id)
        {
            var autor = _autores.FirstOrDefault(a => a.Id == id);
            if (autor != null)
            {
                _autores.Remove(autor);
            }
        }
    }
}
