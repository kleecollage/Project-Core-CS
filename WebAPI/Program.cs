using System.Text;
using Aplicacion.Contratos;
using Aplicacion.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Seguridad.TokenSeguridad;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Registro de CursosOnlineContext
builder.Services.AddDbContext<CursosOnlineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Mediator
builder.Services.AddMediatR(typeof(Consulta.Manejador).Assembly);

// Fluent Validation
builder.Services.AddControllers(opt => {
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
})
    .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

// NetCore Identity
var identity = builder.Services.AddIdentityCore<Usuario>();
var identityBuilder = new IdentityBuilder(identity.UserType, builder.Services);
identityBuilder.AddEntityFrameworkStores<CursosOnlineContext>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();

// Singleton for dependency injection
builder.Services.AddSingleton<ISystemClock, SystemClock>();

// JWT
builder.Services.AddScoped<IJwtGenerador, JwtGenerador>();

// Sesion del  usuario
builder.Services.AddScoped<IUsuarioSesion, UsuarioSesion>();

// Controllers Security
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false, // cualquier persona del mundo puede entrar, de lo contrario se debe especificar los IP's que estan autorizados
        ValidateIssuer = false 
    };
});

var app = builder.Build();

// Ejecutar migraciones automáticamente al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var context = services.GetRequiredService<CursosOnlineContext>();
        context.Database.Migrate();
        DataPrueba.InsertarData(context, userManager).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error durante la migración");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseMiddleware<ManejadorErrorMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
