using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dominio;
using MediatR;
using Aplicacion.Cursos;

namespace WebAPI.Controllers
{
    // http://localhost:5000/api/Cursos
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController: ControllerBase
    {
        private readonly IMediator _mediator;
        public CursosController(IMediator mediator){
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get() {
            return await _mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(int id) {
            return await _mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }
    }
}