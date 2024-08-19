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
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
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