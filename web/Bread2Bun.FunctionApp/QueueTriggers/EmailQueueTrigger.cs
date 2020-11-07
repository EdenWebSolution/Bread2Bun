using System;
using System.Threading.Tasks;
using Bread2Bun.Common.Mailer;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Bread2Bun.FunctionApp.QueueTriggers
{
    public class EmailQueueTrigger
    {
        [FunctionName("EmailQueueTrigger")]
        public async Task SendEmail([QueueTrigger("%Bread2BunEmailQueueName%", Connection = "AzureWebJobsStorage")] EmailBuilder messageBuilder, ILogger log)
        {
            var client = new SendGridClient(Environment.GetEnvironmentVariable("SendGridApiKey"));
            var from = new EmailAddress(Environment.GetEnvironmentVariable("FromEmail"), Environment.GetEnvironmentVariable("Company"));

            var subject = messageBuilder.Subject;
            var to = new EmailAddress(messageBuilder.To);
            var htmlContent = messageBuilder.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
