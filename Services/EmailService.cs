using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class EmailService
    {
        string FromEmail { get; }
        string Password { get; }

        const string HOST = "smtp.gmail.com";

        public EmailService(string fromEmail, string password)
        {
            FromEmail = fromEmail;
            Password = password;
        }

        public async Task SendEmailAsync(string subject, string body, string toAddress, string fromName)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(FromEmail, fromName);
            message.To.Add(new MailAddress(toAddress));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = body;
            smtp.Port = 587;
            smtp.Host = HOST;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(FromEmail, Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
        }
    }
}