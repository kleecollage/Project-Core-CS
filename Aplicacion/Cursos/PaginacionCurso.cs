using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Paginacion;

namespace Aplicacion.Cursos
{
    public class PaginacionCurso
    {
        public class Ejecuta: IRequest<PaginacionModel> {
            public string? Titulo { get; set; }
            public int NumeroPagina { get; set; }
            public int CantidadElementos { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, PaginacionModel>
        {
            private readonly IPaginacion _paginacion;
            public Manejador(IPaginacion paginacion) {
                _paginacion = paginacion;
            }
            public async Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var storedProcedure = "usp_obtener_paginacion";
                var ordenamiento = "Titulo"; // este dato viene directamente de la tabla Curso
                var parametros = new Dictionary<string, object>
                {
                    { "NombreCurso", request.Titulo } // parametro de filtro
                };
                
                return await _paginacion.DevolverPaginacion(
                    storedProcedure,
                    request.NumeroPagina,
                    request.CantidadElementos,
                    parametros,
                    ordenamiento
                );
            }
        }







    }
}