using System;
using System.Collections.Generic;
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
    public class Login
    {
        // Cabecera
        public class Ejecuta: IRequest<UsuarioData> {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        // Validacion de campos
        public class EjecutaValidacion: AbstractValidator<Ejecuta> {
            public EjecutaValidacion() {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        //Logica de Login
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly CursosOnlineContext _context;
            public Manejador(
                UserManager<Usuario> userManager,
                SignInManager<Usuario> signInManager,
                IJwtGenerador jwtGenerador,
                CursosOnlineContext context
                ) {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerador = jwtGenerador;
                _context = context;
                }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }

                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                var resultadoRoles = await _userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);
                
                if (resultado.Succeeded)
                {
                    var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuario.Id)).FirstAsync();
                    if (imagenPerfil != null)
                    {
                        var imagenCliente = new ImagenGeneral
                        {
                            Data = Convert.ToBase64String(imagenPerfil.Contenido),
                            Nombre = imagenPerfil.Nombre,
                            Extension = imagenPerfil.Extension
                        };
                        return new UsuarioData { 
                            NombreCompleto = usuario.NombreCompleto,
                            Token = _jwtGenerador.CrearToken(usuario, listaRoles),
                            UserName = usuario.UserName,
                            Email = usuario.Email,
                            ImagenPerfil = imagenCliente
                        };
                    } else {
                        return new UsuarioData { 
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario, listaRoles),
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        };
                    }
                }
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
            }
        }
    }
}