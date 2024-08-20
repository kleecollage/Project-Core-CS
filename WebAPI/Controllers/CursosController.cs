using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dominio;
using MediatR;
using Aplicacion.Cursos;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<List<Curso>>> Get() {
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        // http://localhost:5000/api/Cursos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(int id) {
            return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }
        
        // http://localhost:5000/api/Cursos
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data) {
            return await Mediator.Send(data);
        }
        
        // http://localhost:5000/api/Cursos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id, Editar.Ejecuta data) {
            data.CursoId = id;
            return await Mediator.Send(data);
        }
        
        // http://localhost:5000/api/Cursos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id) {
            return await Mediator.Send(new Eliminar.Ejecuta{ Id = id });
        }





    }
}