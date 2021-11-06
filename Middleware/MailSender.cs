using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Entities.Support;

namespace Middleware
{
    public static class MailSender
    {
        private static MailAddress From = new ("consolushka@gmail.com", "TestFromName");
        private static MailAddress _to { get; set; }
        private static readonly SmtpClient SmtpClient = new ("smtp.gmail.com");
        private static readonly NetworkCredential Authentication= new ("consolushka@gmail.com", "jup5nkp2FUM.vat4cpd"); 

        public static void ConfirmRegistration(UserModel user)
        {
            try
            {

                // set smtp-client with basicAuthentication
                SmtpClient.UseDefaultCredentials = false;
                SmtpClient.EnableSsl = true;
                NetworkCredential basicAuthenticationInfo = Authentication;
                SmtpClient.Credentials = basicAuthenticationInfo;

                // add from,to mailaddresses
                MailAddress to = new MailAddress(user.Email);
                MailMessage myMail = new MailMessage(From, to);

                // add ReplyTo
                MailAddress replyTo = new MailAddress("consolushka@gmail.com");
                myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = "Test message";
                myMail.SubjectEncoding = Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = $"<form method='post' action='https://localhost:5001/Users/ConfirmRegistration?key={user.ConfirmString}'><button type='submit'>Confirm Registration</button></form>";
                myMail.BodyEncoding = Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;

                SmtpClient.Send(myMail);
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                    ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}