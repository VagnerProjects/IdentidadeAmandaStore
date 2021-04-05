using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Base
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public DateTime DataDeAlteracao { get; set; }
        public bool Lixeira { get; set; }
        public string EntityGuid { get; set; }

        public BaseModel()
        {
            DataDeCadastro = HorarioBrasilia.Get();
            DataDeAlteracao = HorarioBrasilia.Get();
            EntityGuid = Guid.NewGuid().ToString();
        }

        public void EnviarParLixeira() => Lixeira = true;
    }
}
