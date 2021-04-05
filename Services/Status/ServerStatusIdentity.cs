using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Services.Status
{
    public class ServerStatusIdentity : IServerStatusIdentity
    {
        public int Status { get; set; }
        public string Mensagem { get; set; }
    }
}
