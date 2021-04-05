using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Core.Email.Config
{
    public class DispararEmail
    {
        private readonly ServicoDeEmail _serviceDeEmail;

        public DispararEmail(ServicoDeEmail serviceDeEmail)
        {
            _serviceDeEmail = serviceDeEmail;
        }

        public async Task Disparar()
        {
            await _serviceDeEmail.EnviarEmail();
        }
    }
}
