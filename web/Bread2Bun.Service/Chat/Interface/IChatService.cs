using Bread2Bun.Common;
using Bread2Bun.Common.Enum;
using Bread2Bun.Service.Chat.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Chat.Interface
{
    public interface IChatService
    {
        Task<ChatModel> SendMessage(MessageModel messageModel);
        Task<PaginationModel<ChatModel>> GetAllChatById(PaginationBase paginationBase, long to);
        Task<PaginationModel<ChatSummaryModel>> GetAllChatSummary();
        Task ToggleMessageReadStatus(long fromId, MessageStatus status);
        Task<int> GetCountOfAllUnredMessages(long toId);
    }
}
