using System.Net;
using System.Net.Mail;

namespace Middleware
{
    public class MailSender
    {
        private MailAddress _from = new MailAddress("vlad@kerby.ru", "Андроид ShoeApiVue");
        private MailAddress _to { get; set; }
        private SmtpClient _smtpClient = new SmtpClient("smtp.yandex.ru", 465);

        public MailSender(string toAddress)
        {
            _to = new MailAddress(toAddress);
            _smtpClient.Credentials = new NetworkCredential("vlad@kerby.ru", "x>6^aR&8RMY:Mpc3vYFP#ot%");
            _smtpClient.EnableSsl = true;
        }

        public void ConfirmRegistration()
        {
            MailMessage mailMessage = new MailMessage(_from, _to);
            mailMessage.Subject = "Подтверждение регистрации";
            mailMessage.Body = "<h1>Вам пришло письмо</h2>";
            mailMessage.IsBodyHtml = true;
            _smtpClient.Send(mailMessage);
        }
    }
}