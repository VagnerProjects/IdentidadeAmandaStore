using IdentidadeAmandaStore.Domain.Contexto;
using IdentidadeAmandaStore.Extensions;
using IdentidadeAmandaStore.Services;
using IdentidadeAmandaStore.Services.ValidacaoIdentity.LoginValidate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore
{
    public class DependencyInjection
    {
        public static void Dependencias(IServiceCollection services)
        {
            services.AddScoped<IRegistrarUsuario, RegistrarUsuario>();
            services.AddScoped<IValidateLogin, ValidateLogin>();
            services.AddTransient<IdentidadeAmandaStoreContext>();
            services.AddTransient<IdentityErrorDescriber, IdentityMensagens>();
        }
    }
}
