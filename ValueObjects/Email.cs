using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.ValueObjects
{
    public class Email
    {
        public const int EnderecoMaxLenght = 254;
        public string Endereco { get; private set; }
        protected Email() { }

        public Email(string endereco)
        {
            Endereco = endereco;
        }

        public string GetEndereco(string email)
        {
            return Endereco;
        }
        public void SetEndereco(string EnderecoDeEmail)
        {
            try
            {
                var regex = new Regex(@"[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+");
                var match = regex.Match(EnderecoDeEmail);

                if (match.Success)
                    Endereco = EnderecoDeEmail;
                else
                    throw new Exception("E-mail inválido ou incorreto!");
            }
            catch
            {
                throw new Exception("E-mail inválido ou incorreto!");
            }
        }

        public override bool Equals(object obj)
        {
            var email = obj as Email;
            return email != null &&
                   Endereco.Trim().ToLower() == email.Endereco.Trim().ToLower();
        }

        public void ValidaEmailVazioParaLogin()
        {
            if (string.IsNullOrEmpty(Endereco))
            {
                throw new Exception("E-mail inválido ou incorreto!");
            }
        }
    }
}
