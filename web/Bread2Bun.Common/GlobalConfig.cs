using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bread2Bun.Common
{
    public class GlobalConfig
    {

        //// development
        //public static string APIBaseUrl = "http://localhost:54969";
        //public static string PresentationBaseUrl = "http://localhost:4200";

        //// production
        //public static string APIBaseUrl = "https://www.bread2bun.com";
        //public static string PresentationBaseUrl = "https://www.bread2bun.com";

        // test
        public static string APIBaseUrl = "https://breadtobun.azurewebsites.net";
        public static string PresentationBaseUrl = "https://breadtobun.azurewebsites.net";

        public static string AzureWebJobsStorage { get; set; }
        public static string Bread2BunEmailQueueName { get; set; }



        public static void Register(IConfiguration configuration)
        {
            AzureWebJobsStorage = configuration["Azure:StorageAccount:AzureWebJobsStorage"];
            Bread2BunEmailQueueName = configuration["Azure:StorageAccount:Bread2BunEmailQueueName"];
        }
    }
}
