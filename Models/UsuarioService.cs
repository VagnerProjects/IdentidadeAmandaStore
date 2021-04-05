using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Models
{
    public class UsuarioService
    {
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        public string Cpf { get; set; }

        private string _Email;

        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get { return _Email; } set { _Email = string.IsNullOrWhiteSpace(value) ? null : value; } }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string SenhaConfirmacao { get; set; }

        public string RgNumero { get; set; }

        public string RgOrgaoEmissor { get; set; }

        public string RgPaisEmissor { get; set; }

        public DateTime? RgDataDeEmissao { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Foto { get; set; }

        public int Sexo { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string TipoUsuario { get; set; }

        public DateTime DataDeNascimento { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Numero { get; set; }

        public string Cep { get; set; }


        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }


        public int calcularIdade()
        {
            if (DataDeNascimento == null)
                return 0;

            int idade = DateTime.Now.Year - DataDeNascimento.Year;
            if (DateTime.Now.Month < DataDeNascimento.Month || (DateTime.Now.Month == DataDeNascimento.Month && DateTime.Now.Day < DataDeNascimento.Day))
                idade--;

            return idade;

        }
    }
}
