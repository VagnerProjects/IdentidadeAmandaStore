using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Services.Status
{
    public interface IServerStatusIdentity
    {
        int Status { get; set; }
        string Mensagem { get; set; }
    }
}
