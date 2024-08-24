using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Documentos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class DocumentoController: MiControllerBase
    {
        // localhost:5000/api/Documento
        [HttpPost]
        public async Task<ActionResult<Unit>> SubirArchivo(SubirArchivo.Ejecuta parametros) {
            return await Mediator.Send(parametros);
        }

        // localhost:5000/api/Documento/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArchivoGenerico>> ObtenerDocumento(Guid id) {
            return await Mediator.Send(new ObtenerArchivo.Ejecuta { Id = id });
        }

    }
}