using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager) {
            if (!usuarioManager.Users.Any())
            { 
                // Las propiedades que no estan definidas en la clase Usuario
                // vienen por defecto  en la clase IdentityUser
                var usuario = new Usuario { 
                    NombreCompleto = "Vaxi Drez",
                    UserName = "vaxidrez",
                    Email = "vaxi.drez@gmail.com"
                    };
                await usuarioManager.CreateAsync(usuario, "Password123$");
            }
        }
    }
}