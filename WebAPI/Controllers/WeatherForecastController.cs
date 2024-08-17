using Dominio;
using Microsoft.AspNetCore.Mvc;
using Persistencia;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
// http://localhost:5000/WeatherForecasts
public class WeatherForecastController : ControllerBase
{
    // inyeccion de dependencias
    private readonly CursosOnlineContext context;
    public WeatherForecastController(CursosOnlineContext _context) {
        this.context = _context;
    }
    [HttpGet]
    public IEnumerable<Curso> Get() {
        return context.Curso.ToList();
    }
}
