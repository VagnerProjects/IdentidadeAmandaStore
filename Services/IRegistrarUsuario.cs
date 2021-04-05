using IdentidadeAmandaStore.Models;
using IdentidadeAmandaStore.Services.Status;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Services
{
    public interface IRegistrarUsuario
    {
        Task<IServerStatusIdentity> RegistrarUsuarioService(UsuarioService usuarioRegistro);

        Task<IServerStatusIdentity> RegistrarIdentidade(UsuarioRegistro AutenticacaoModel);
    }
}
