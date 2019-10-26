using Bread2Bun.Common;
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
        Task<PaginationModel<ChatModel>> GetAllChatById(PaginationBase paginationBase, long to);
        Task<PaginationModel<ChatSummaryModel>> GetAllChatSummary();
    }
}
