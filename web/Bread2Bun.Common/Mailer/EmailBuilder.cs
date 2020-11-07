using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Bread2Bun.Common.Mailer
{
    public class EmailBuilder
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public string To { get; set; }

        public static async Task SendEmailByQueueAsync(EmailBuilder messageBuilder)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(GlobalConfig.AzureWebJobsStorage);
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                CloudQueue queue = queueClient.GetQueueReference(GlobalConfig.Bread2BunEmailQueueName);
                await queue.CreateIfNotExistsAsync();

                CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(messageBuilder));
                await queue.AddMessageAsync(message);
            }
            catch (System.Exception ex)
            {
                throw;
            }

        }
    }
}
