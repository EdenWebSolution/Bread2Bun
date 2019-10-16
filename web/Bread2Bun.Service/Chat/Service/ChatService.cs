using Bread2Bun.Common;
using Bread2Bun.Data;
using Bread2Bun.Service.Chat.Interface;
using Bread2Bun.Service.Chat.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Chat.Service
{
    public class ChatService : IChatService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly UserResolverService userResolverService;

        public ChatService(Bread2BunContext bread2BunContext, UserResolverService userResolverService)
        {
            this.bread2BunContext = bread2BunContext;
            this.userResolverService = userResolverService;
        }
        public async Task SendMessage(MessageModel messageModel)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(messageModel));
        }

        public async Task<PaginationModel<ChatModel>> GetAllChatById(PaginationBase paginationBase, long to)
        {
            var query = bread2BunContext.Message.Where(w => w.FromId == userResolverService.UserId && w.ToId == to);

            var totalRecords = await query.AsNoTracking().CountAsync();

            query = query.OrderByDescending(o => o.CreatedOn).Skip(paginationBase.Skip).Take(paginationBase.Take);

            var result = query.Select(s => new ChatModel
            {
                Date = s.CreatedOn,
                FromId = s.FromId,
                Message = s.Text,
                ToId = s.ToId,
                FromUserName = s.From.UserName,
                ToUserName = s.To.UserName
            });

            var resultSet = await result.AsNoTracking().ToListAsync();


            var resultData = new PaginationModel<ChatModel>
            {
                Details = resultSet,
                TotalRecords = totalRecords
            };

            return resultData;
        }
    }
}
