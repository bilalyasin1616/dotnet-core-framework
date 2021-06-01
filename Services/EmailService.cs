using System.Net;
using System.Net.Mail;

namespace Framework.Services
{
    public class EmailService
    {
        public string _fromEmail { get; set; }
        public string _password { get; set; }

        public EmailService(string fromEmail, string password)
        {
            _fromEmail = fromEmail;
            _password = password;
        }

        public void SendEmail(string subject, string body, string toAddress, string fromName)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_fromEmail, fromName);
            message.To.Add(new MailAddress(toAddress));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = body;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_fromEmail, _password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}