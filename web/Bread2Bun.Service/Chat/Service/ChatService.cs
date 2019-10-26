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
            try
            {
                var users = new[] { userResolverService.UserId, messageModel.ToId };

                Array.Sort(users);

                var chatGroup = string.Join(",", users);
                var messageThread = bread2BunContext.MessageThreaad.FirstOrDefault(f => f.ChatGroup == chatGroup);

                if (messageThread is null)
                {
                    messageThread = new MessageThreaad();
                    messageThread.Create(chatGroup);
                    await bread2BunContext.AddAsync(messageThread);
                }

                var message = new Message().Create(userResolverService.UserId, messageModel.ToId, messageModel.Text, messageThread.Id);
                await bread2BunContext.AddAsync(message);
                await bread2BunContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<PaginationModel<ChatModel>> GetAllChatById(PaginationBase paginationBase, long to)
        {
            var users = new[] { userResolverService.UserId, to };

            Array.Sort(users);

            var chatGroup = string.Join(",", users);

            var query = bread2BunContext.Message.Where(w => w.MessageThreaad.ChatGroup == chatGroup);

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

            var query = bread2BunContext.Message.Include(i => i.To).Include(i => i.From).Where(w => w.FromId == userResolverService.UserId || w.ToId == userResolverService.UserId);


            var unread = await query.Where(w => !w.IsRead && w.ToId == userResolverService.UserId)
                                               .GroupBy(g => g.FromId).Select(s => new { s.Key, count = s.Count() })
                                               .AsNoTracking()
                                               .ToListAsync();

            query = query.GroupBy(g => g.ThreadId).Select(s => s.OrderByDescending(o => o.CreatedOn).First()).AsNoTracking();

            foreach (var item in await query.AsNoTracking().ToListAsync())
            {
                summaryList.Add(new ChatSummaryModel
                {
                    ToUserId = item.ToId == userResolverService.UserId ? item.FromId : item.ToId,
                    FromUserId = item.FromId == userResolverService.UserId ? item.ToId : item.FromId,
                    LastMessage = item.Text,
                    Name = item.To.FullName,
                    UserName = item.ToId == userResolverService.UserId ? item.From.UserName : item.To.UserName,
                    Date = item.CreatedOn,
                    UnReadCount = unread.FirstOrDefault(s => s.Key == item.ToId)?.count ?? 0
                });
            }

            var result = new PaginationModel<ChatSummaryModel>
            {
                Details = summaryList.OrderByDescending(o => o.Date)
            };

            return result;
        }
    }
}
