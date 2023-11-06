using System;

namespace ApiCrud.DTOs
{
    public class LibroDTO
    {
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public Guid AutorId { get; set; }
    }
}
