using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dominio;
using MediatR;
using Aplicacion.Cursos;
using Microsoft.AspNetCore.Authorization;
using Persistencia.DapperConexion.Paginacion;

namespace WebAPI.Controllers
{
    // http://localhost:5000/api/Cursos
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController: MiControllerBase //ControllerBase
    {
        // Ahora inyectamos IMediator desde nuestra clase MiControllerBase
        // private readonly IMediator _mediator;
        // public CursosController(IMediator mediator){
        //     _mediator = mediator;
        // }

        // http://localhost:5000/api/Cursos
        [HttpGet]
        public async Task<ActionResult<List<CursoDto>>> Get() {
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        // http://localhost:5000/api/Cursos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> Detalle(Guid id) {
            return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }
        
        // http://localhost:5000/api/Cursos
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data) {
            return await Mediator.Send(data);
        }
        
        // http://localhost:5000/api/Cursos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta data) {
            data.CursoId = id;
            return await Mediator.Send(data);
        }
        
        // http://localhost:5000/api/Cursos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id) {
            return await Mediator.Send(new Eliminar.Ejecuta{ Id = id });
        }

        // http://localhost:5000/api/Cursos/report
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionCurso.Ejecuta data) {
            return await Mediator.Send(data);
        }




    }
}