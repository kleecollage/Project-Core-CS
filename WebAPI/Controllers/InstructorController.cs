using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Instructores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Instructor;

namespace WebAPI.Controllers
{
    public class InstructorController: MiControllerBase
    {
        // http://localhost:5000/api/Instructor
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> ObtenerInstructores() {
            return await Mediator.Send(new Consulta.Lista());
        }

        // http://localhost:5000/api/Instructor
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data) {
            return await Mediator.Send(data);
        }
        
        // http://localhost:5000/api/Instructor/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Actualizar(Guid id, Editar.Ejecuta data) {
            data.InstructorId = id;
            return await Mediator.Send(data);
        }

        // http://localhost:5000/api/Instructor/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id) {
            return await Mediator.Send(new Eliminar.Ejecuta{Id = id});
        }

        // http://localhost:5000/api/Instructor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorModel>> ObtenerPorId(Guid id) {
            return await Mediator.Send(new ConsultaId.Ejecuta { Id = id });
        }

    }
}