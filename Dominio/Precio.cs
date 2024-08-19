using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Precio
    {
        public Guid PrecioId { get; set; }
        
        [Column(TypeName ="decimal(18,4)")]
        public decimal PrecioActual { get; set; }
        
        [Column(TypeName ="decimal(18,4)")]
        public decimal Promocion { get; set; }
        public Guid CursoId { get; set; }
        public Curso Curso { get; set; } // Relacion 1:1 entre Precio y Curso
    }
}