using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Caalinder.Observer
{
    public class ObserverModel
    {
        public string _email;
        public ObserverModel(string email)
        {
            this._email = email;
        }

        public void Update(int like)
        {
            if (like % 5 == 0)
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("contatocavalinder@gmail.com", "Cavalinder123");
                MailMessage mail = new MailMessage();
                mail.Sender = new System.Net.Mail.MailAddress("contatocavalinder@gmail.com", "ENVIADOR");
                mail.From = new MailAddress("contatocavalinder@gmail.com", "Cavalinder");
                mail.To.Add(new MailAddress( _email, "Usuário"));
                mail.Subject = "Contato";
                mail.Body = "Parabéns! Seu cavalo atingiu" + like + "Likes";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                try
                {
                    client.Send(mail);
                }
                catch (System.Exception erro)
                {
                    //trata erro
                }
                finally
                {
                    mail = null;
                }
            }
        }

    }
}