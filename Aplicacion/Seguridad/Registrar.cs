using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta: IRequest<UsuarioData> {
            public string NombreCompleto { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class EjecutaValidador: AbstractValidator<Ejecuta> {
            public EjecutaValidador() {
                RuleFor(x => x.NombreCompleto).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();    
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursosOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador) {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            { // x representa los registros dentro de la tabla Users
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();  // Esta tabla se genera automaticamente cuando migramos la db. En la db esta tabla se muestra como "dbo.AspNetUsers"
                if (existe)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ya existe un usuario registrado con este email" });
                }

                var existeUserName = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (existeUserName)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ya existe un usuario con este username" });
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.NombreCompleto,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if (resultado.Succeeded)
                {
                    return new UsuarioData {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario, null),
                        UserName = usuario.UserName,
                        Email = usuario.Email
                    };
                }
                throw new Exception("No se pudo crear al nuevo usuario");
            }
        }



    }
}