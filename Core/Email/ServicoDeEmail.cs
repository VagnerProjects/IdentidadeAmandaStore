using IdentidadeAmandaStore.Core.Email.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Core.Email
{
    public abstract class ServicoDeEmail : SMTPConfiguration
    {
        public readonly MailMessage _Email;

        public abstract string SubstituirValores();

        public abstract Task EnviarEmail();

        public void GerarEmail(string assunto, string conteudo, string enviadoPor, string titulo)
        {
            _Email.From = new MailAddress(enviadoPor,titulo);
            _Email.Subject = assunto;
            _Email.IsBodyHtml = true;
            _Email.Body = "<h1>Olá Vagner, boa noite, recupere seu acesso aqui: </h1>"+ $" <a href='{conteudo}'>Token <a/>";
          
        }
        public ServicoDeEmail() => _Email = new MailMessage();

    }
}
