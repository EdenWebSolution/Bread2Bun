using Bread2Bun.Service.Chat.Interface;
using Bread2Bun.Service.Chat.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Chat.Service
{
    public class ChatService : IChatService
    {
        public async Task SendMessage(MessageModel messageModel)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(messageModel));
        }

       
    }
}
