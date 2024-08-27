using Aplicacion.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class ExportarDocumentoController: MiControllerBase
    {
        // http://localhost:5000/api/ExportarDocumento
        [HttpGet]
        public async Task<ActionResult<Stream>> GetTask() {
            return await Mediator.Send(new ExportPDF.Consulta());
        }
    }
}