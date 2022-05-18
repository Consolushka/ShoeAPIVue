using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Shop.Data.Models;

namespace Shop.API.Core
{
    public static class MailSender
    {
        private static MailAddress From = new ("consolushka@gmail.com", "TestFromName");
        private static MailAddress _to { get; set; }
        private static readonly SmtpClient SmtpClient = new ("smtp.gmail.com");
        private static readonly NetworkCredential Authentication= new ("consolushka@gmail.com", "jup5nkp2FUM.vat4cpd"); 

        public static void ConfirmRegistration(User user)
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
                myMail.Body = $"<a href='http://localhost:8080/#/user/confirm?key={user.ConfirmString}'>Confirm Registration</a>";
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
        }

        public static void ConfirmUpdate(User user)
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
                myMail.Body = $"<a href='http://localhost:8080/#/user/confirm?key={user.ConfirmString}'>Confirm Shop.Data Updation</a><a href='http://localhost:8080/#/user/confirm?key={user.ConfirmString}'>Confirm Shop.Data Updation</a>";
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
        }
    }
}