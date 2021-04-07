using IdentidadeAmandaStore.Models;
using IdentidadeAmandaStore.Services.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Services.ValidacaoIdentity.LoginValidate
{
    public interface IValidateLogin
    {
        Task<IServerStatusIdentity> Validar(UsuarioLogin usuarioLogin);
    }
}
