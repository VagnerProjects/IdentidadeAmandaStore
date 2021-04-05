using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.ValueObjects
{
    public class Senha
    {
        private const int MaxLength = 100;

        private const int MinLength = 5;
        public string Password { get; set; }
        protected Senha() { }

        public Senha(string StrPassWord)
        {
            if (string.IsNullOrEmpty(StrPassWord))
                this.Password = "";

            Password = StrPassWord;
        }

        public void AtribuirValorSenha(string valor) => Password = valor;

        //public string ObterSenhaLimpa()
        //{
        //    return Criptograph.Decrypt(Password);
        //}

        public void SetPassword(string pass)
        {
            ValidaSizePassword(pass, MaxLength, MinLength);

            //Password = Criptograph.Encrypt(pass);
            //Password = WebUtility.UrlDecode(Password);
        }

        public override bool Equals(object obj)
        {

            if (obj is Senha)
            {
                var enderecoObj = (Senha)obj;

                if (enderecoObj.Password == Password)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return -1013108008 + EqualityComparer<string>.Default.GetHashCode(Password);
        }

        public static void ValidaSizePassword(string value, int maxLenght, int minLength)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length < minLength || value.Length > maxLenght)
                    throw new Exception("Campo senha deve ter mínimo " + minLength + " e máximo de " + maxLenght + " dígitos!");
            }
            else
                throw new Exception("Campo senha não pode ser vazio!");
        }
    }
}
