using Bread2Bun.Service.Chat.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Chat.Interface
{
    public interface IChatService
    {
        Task SendMessage(MessageModel messageModel);
    }
}
