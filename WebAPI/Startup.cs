using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistencia;

namespace YourNamespace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método se usa para configurar los servicios que la aplicación va a utilizar.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar controladores para la API.
            services.AddControllers();

            // Si estás utilizando Entity Framework Core, aquí es donde lo configuras.
            services.AddDbContext<CursosOnlineContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Configuración de CORS, autenticación, etc.
            // services.AddCors();
            // services.AddAuthentication();

            // Configura la inyección de dependencias para servicios personalizados.
            // services.AddScoped<IYourService, YourService>();

            // Agregar Swagger para la documentación de la API.
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // Este método se usa para configurar el pipeline de solicitud HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Usar la página de excepciones de desarrollador en modo de desarrollo.
                app.UseDeveloperExceptionPage();

                // Habilitar Swagger y Swagger UI.
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
            }
            else
            {
                // Usar manejo de excepciones genérico en producción.
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Middleware para redirigir HTTP a HTTPS.
            app.UseHttpsRedirection();

            // Middleware para servir archivos estáticos (si es necesario).
            app.UseStaticFiles();

            // Middleware para enrutar solicitudes.
            app.UseRouting();

            // Configuración de CORS (si es necesario).
            // app.UseCors();

            // Configuración de autenticación y autorización (si es necesario).
            // app.UseAuthentication();
            // app.UseAuthorization();

            // Configuración de endpoints para controladores.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
