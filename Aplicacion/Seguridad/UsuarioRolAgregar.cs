using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class UsuarioRolAgregar
    {
        public class Ejecuta: IRequest {
            // unicamente debuelve un flag
            public string UserName { get; set; }
            public string RolNombre { get; set; }
        }

        public class EjecutaValidacion: AbstractValidator<Ejecuta> {
            public EjecutaValidacion() {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager) {
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolNombre);
                if (role == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El rol no existe" });
                }

                var usuaroIden = await _userManager.FindByNameAsync(request.UserName);
                if (usuaroIden == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El usuario no existe" });

                }

                var resultado = await _userManager.AddToRoleAsync(usuaroIden, request.RolNombre);
                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo agragar el rol al usuario");


            }
        }



    }
}