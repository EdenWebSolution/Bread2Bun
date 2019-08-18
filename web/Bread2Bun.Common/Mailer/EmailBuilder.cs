using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Common.Mailer
{
    public class EmailBuilder
    {
        private static MailMessage mailMessage;
        private static SmtpClient smtpClient;

        public EmailBuilder(IConfiguration configuration)
        {
            smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(configuration["Email:credentials:username"], configuration["Email:credentials:password"]),
                Timeout = 10000,
            };
        }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public string[] To { get; set; }

        public static async Task SendEmailAsync(EmailBuilder messageBuilder)
        {
            mailMessage = new MailMessage
            {
                IsBodyHtml = messageBuilder.IsBodyHtml,
                Subject = messageBuilder.Subject,
                Body = messageBuilder.Body
            };
            mailMessage.From = new MailAddress(mailMessage.From.Address, mailMessage.From.DisplayName);

            foreach (var emailAddress in messageBuilder.To)
                mailMessage.To.Add(emailAddress);

            await smtpClient.SendMailAsync(mailMessage);
            mailMessage.Dispose();
        }
    }
}
