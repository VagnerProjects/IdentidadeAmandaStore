using IdentidadeAmandaStore.Domain.Contexto;
using IdentidadeAmandaStore.Extensions;
using IdentidadeAmandaStore.Models;
using IdentidadeAmandaStore.Services.Status;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Services
{
    public class RegistrarUsuario : IRegistrarUsuario
    {
        private readonly UserManager<IdentityUser> _userManager;
       
        public RegistrarUsuario(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            
        }
        public async Task<IServerStatusIdentity> RegistrarIdentidade(UsuarioRegistro AutenticacaoModel)
        {
            await _userManager.FindByEmailAsync(AutenticacaoModel.Email);

            //if (findUser != null)
            //    return new ServerStatusIdentity() { Mensagem = new IdentityMensagens().LoginAlreadyAssociated().Description, Status = 1 };

            var userTipo = AutenticacaoModel.TipoUsuario;

            var user = IdentityUserFactoryAutentic(AutenticacaoModel);

            AutenticacaoModel.UsuarioId = Guid.Parse(user.Id);

            var result = await _userManager.CreateAsync(user, AutenticacaoModel.Senha);
         
             var motivo = new List<string>();

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                    motivo.Add(item.Description);                   

                return new ServerStatusIdentity() { Mensagem = "Não foi possível criar a identidade!", ListMensagem = motivo, Status = 1 };
            }
                
            await _userManager.AddClaimAsync(user, new Claim("TipoUsuario", userTipo));

            return new ServerStatusIdentity() { Mensagem = "Identidade Criada!", Status = 0 };
        }

        public async Task<IServerStatusIdentity> RegistrarUsuarioService(UsuarioService usuarioService)
        {
            var tipoUser = usuarioService.TipoUsuario;
            var user = IdentityUserFactory(usuarioService);

            usuarioService.UsuarioId = Guid.Parse(user.Id);

            var result = await _userManager.CreateAsync(user, usuarioService.Senha);

            var identity = await _userManager.AddClaimAsync(user, new Claim("TipoUsuario", tipoUser));

            return new ServerStatusIdentity() { Mensagem = "Ok", Status = 0 };

        }

        private IdentityUser IdentityUserFactory(UsuarioService usuarioRegistro)
        {

            return new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

        }

        private IdentityUser IdentityUserFactoryAutentic(UsuarioRegistro usuarioRegistro)
        {

            return new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

        }
    }
}
