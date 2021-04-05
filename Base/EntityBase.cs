using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Base
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public DateTime DataDeAlteracao { get; set; }
        public bool Lixeira { get; set; }

        public EntityBase()
        {
            DataDeCadastro = HorarioBrasilia.Get();
            DataDeAlteracao = HorarioBrasilia.Get();
        }

        public void EnviarParLixeira() => Lixeira = true;
    }
}
