using Bread2Bun.Common;
using Bread2Bun.Data;
using Bread2Bun.Domain.Chat;
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
            var users = new[] { userResolverService.UserId, messageModel.ToId };

            Array.Sort(users);

            var chatGroup = string.Join(",", users);
            var messaeThread = bread2BunContext.MessageThreaad.FirstOrDefault(f => f.ChatGroup == chatGroup);
            var message = new Message().Create(userResolverService.UserId, messageModel.ToId, messageModel.Text);
            await bread2BunContext.Message.AddAsync(message);
            await bread2BunContext.SaveChangesAsync();
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

        public async Task<PaginationModel<ChatSummaryModel>> GetAllChatSummary()
        {
            var summaryList = new List<ChatSummaryModel>();

            var query = bread2BunContext.Message.Include(i => i.To).Where(w => w.FromId == userResolverService.UserId || w.ToId == userResolverService.UserId);


            var unread = await query.Where(w => !w.IsRead && w.ToId == userResolverService.UserId)
                                               .GroupBy(g => g.FromId).Select(s => new { s.Key, count = s.Count() })
                                               .AsNoTracking()
                                               .ToListAsync();

            query = query.GroupBy(g => g.ToId).Select(s => s.OrderByDescending(o => o.CreatedOn).First()).AsNoTracking();

            foreach (var item in await query.AsNoTracking().ToListAsync())
            {
                summaryList.Add(new ChatSummaryModel
                {
                    ToUserId = item.ToId,
                    FromUserId = item.FromId,
                    LastMessage = item.Text,
                    Name = item.To.FullName,
                    UserName = item.To.UserName,
                    Date = item.CreatedOn,
                    UnReadCount = unread.FirstOrDefault(s => s.Key == item.ToId)?.count ?? 0
                });
            }

            var result = new PaginationModel<ChatSummaryModel>
            {
                Details = summaryList
            };

            return result;
        }
    }
}
