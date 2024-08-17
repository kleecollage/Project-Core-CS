using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
// http://localhost:5000/WeatherForecasts
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get() {
        string[] nombres = new[] { "Fabiana", "Rolando", "Maria", "Juana" };
        return nombres;
    }
}
