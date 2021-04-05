using IdentidadeAmandaStore.Base;
using IdentidadeAmandaStore.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Models
{
    public class UsuarioRegistro
    {
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string TipoUsuario { get; set; }

        private string _Email;

        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get { return _Email; } set { _Email = string.IsNullOrWhiteSpace(value) ? null : value; } }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Senha { get; set; }

    }
}
