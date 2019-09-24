using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Bread2Bun.Common.Mailer
{
    public class EmailBuilder
    {
        private static MailMessage mailMessage;
        private static SmtpClient smtpClient;
        private static string From;

        private static string DisplayName => "Bread 2 Bun";


        public EmailBuilder(IConfiguration configuration)
        {

            From = configuration["Email:credentials:username"];

            smtpClient = new SmtpClient()
            {
                Host = "mail.bread2bun.com",
                Port = 25,
                //EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(From, configuration["Email:credentials:password"]),
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
            mailMessage.From = new MailAddress(From, DisplayName);

            foreach (var emailAddress in messageBuilder.To)
                mailMessage.To.Add(emailAddress);

            await smtpClient.SendMailAsync(mailMessage);
            mailMessage.Dispose();
        }
    }
}
