using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class SendEmail
    {
        public string SendingEmail(string emailTo, string token)
        {
            try
            {
                string emailFrom = "sanjanams812@gmail.com";

                MailMessage message = new MailMessage(emailFrom, emailTo);
                string mailBody = "Generated Token is: " + token;
                message.Subject = "Token Generated For Forgot Password. Will Expire in 15 minutes";
                message.Body = mailBody.ToString();
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                System.Net.NetworkCredential credential = new
                System.Net.NetworkCredential("sanjanams812@gmail.com", "zaqlowifyddahvnr");

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credential;

                smtpClient.Send(message);

                return emailTo;
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }
    }
}
