using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comentario
    {
        public Guid ComentarioId { get; set; } // Guid = Global Unique Identifier
        public string? Alumno { get; set; }
        public int? Puntaje { get; set; }
        public string? ComentarioTexto { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Guid CursoId { get; set; }
        public Curso Curso { get; set; } // Relacion 1:N entre Curso y Comentario
    }
}