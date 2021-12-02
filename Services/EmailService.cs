using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class EmailService
    {
        private readonly string fromEmail;

        private readonly string password;

        private const string HOST = "smtp.gmail.com";

        public EmailService(IConfiguration configuration)
        {
            if (configuration["SmtpEmailConfiguration:Email"] == null || configuration["SmtpEmailConfiguration:Password"] == null)
                throw new Exception("From email or from email password is not defined in configuration make sure to have SmtpEmailConfiguration:Email and SmtpEmailConfiguration:Password in your configuration");
            fromEmail = configuration["SmtpEmailConfiguration:Email"];
            password = configuration["SmtpEmailConfiguration:Password"];
        }

        public virtual async Task SendEmailAsync(string subject, string body, string toAddress, string fromName)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(fromEmail, fromName);
            message.To.Add(new MailAddress(toAddress));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html
            message.Body = body;
            smtp.Port = 587;
            smtp.Host = HOST;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromEmail, password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
        }
    }
}