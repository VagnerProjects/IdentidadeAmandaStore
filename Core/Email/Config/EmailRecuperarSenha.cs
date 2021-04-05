using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Core.Email.Config
{
    public class EmailRecuperarSenha : ServicoDeEmail
    {
        private string emailUsuario;
        private string _Assunto = "Recuperar senha";

        public EmailRecuperarSenha(string token, string email)
        {
            emailUsuario = email;
            var conteudo = token;
            GerarEmail(_Assunto, conteudo, "administracao@amandastore", "Amanda Store");
        }

        public override async Task EnviarEmail()
        {
            _Email.To.Add(emailUsuario);
            await Task.Run(() => base.Send(_Email));
        }

        public override string SubstituirValores()
        {
            var CaminhoDoHtml = "wwwroot\\Email\\RecuperarSenha.html";

            //var conteudoDoHtmlDoEmail = File.ReadAllText(CaminhoDoHtml);

            //conteudoDoHtmlDoEmail = conteudoDoHtmlDoEmail.Replace("{nome}", usuario.nomeUsuario);
            //conteudoDoHtmlDoEmail = conteudoDoHtmlDoEmail.Replace("{link}", $"{caminhoUrl}{usuario.UsuarioId}");

            return CaminhoDoHtml;
        }
    }
}
