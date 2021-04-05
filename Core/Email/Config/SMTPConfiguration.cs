using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Core.Email.Config
{
    public class SMTPConfiguration : SmtpClient
    {
        public SMTPConfiguration()
          : base("smtp.gmail.com", 587)
        {
            EnableSsl = true;
            UseDefaultCredentials = false;
            Credentials = new NetworkCredential("mandyamabts@gmail.com", "amandalinda0102");
            DeliveryMethod = SmtpDeliveryMethod.Network;
            
        }
    }
}
