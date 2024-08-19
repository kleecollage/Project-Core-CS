using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class CursoInstructor
    {
        public Guid CursoId { get; set; }
        public Curso Curso { get; set; } // Relacion N:N entre Curso e Instructor
        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; } // Relacion N:N entre Curso e Instructor
    
    }
}