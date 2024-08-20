using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta: IRequest {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // se eliminan las referencias de los instructores que puedan existir en el curso
                var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);
                foreach (var instructor in instructoresDB)
                {
                    _context.CursoInstructor.Remove(instructor);
                }

                // se eliminan los comentarios referenciados del curso
                var comentariosDB = _context.Comentario.Where(x => x.CursoId == request.Id);
                foreach (var comentario in comentariosDB)
                {
                    _context.Comentario.Remove(comentario);
                }


                // se eliminan las referencias de los precios del curso
                var precioDB = _context.Precio.Where(x => x.CursoId == request.Id).FirstOrDefault();
                if (precioDB != null)
                {
                    _context.Precio.Remove(precioDB);
                }

                // se elimina el curso
                var curso = await _context.Curso.FindAsync(request.Id);
                if (curso == null)
                {
                    // throw new Exception("No se puede eliminar curso"); // Excepcion regular de C#
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso" });
                }
                _context.Remove(curso);
               
                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0) return Unit.Value;
                else throw new Exception("No se pudo eliminar el curso");

            }
        }
    }
}