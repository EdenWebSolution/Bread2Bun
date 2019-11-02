using Bread2Bun.Common;
using Bread2Bun.Service.Chat.Interface;
using Bread2Bun.Service.Chat.Model;
using Bread2Bun.Service.Security.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bread2Bun.Web.AppHubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly IChatService chatService;
        private readonly AllConnectedUsers allConnectedUsers;
        private readonly ISecurityService securityService;
        private readonly UserResolverService userResolverService;

        public ChatHub(IChatService chatService, AllConnectedUsers allConnectedUsers, ISecurityService securityService, UserResolverService userResolverService)
        {
            this.chatService = chatService;
            this.allConnectedUsers = allConnectedUsers;
            this.securityService = securityService;
            this.userResolverService = userResolverService;
        }

        //public async Task SendMessageToAll(MessageModel msg)
        //{
        //    //var chat = await chatService.SendMessage(msg);
        //    //await Clients.All.SendAsync("ReceiveMessage", chat);
        //}

        public async Task SendMessage(MessageModel msg)
        {
            var chat = await chatService.SendMessage(msg);

            if (!string.IsNullOrEmpty(msg.ClientUniqueId))
            {
                await Clients.Client(msg.ClientUniqueId).SendAsync("ReceiveMessage", chat);

                await SendGlobalMessageNotificationToUniqueUser(msg.ClientUniqueId, msg.ToId);
            }
        }


        public async Task SendGlobalMessageNotificationToUniqueUser(string clientUniqueId, long toId)
        {
            var allUnreadMessagCount = await chatService.GetCountOfAllUnredMessages(toId);
            await Clients.Client(clientUniqueId).SendAsync("newMessageNotification", allUnreadMessagCount);
        }

        public async Task GetMyGlobalMessageNotificationToUniqueUser(long toId)
        {
            var allUnreadMessagCount = await chatService.GetCountOfAllUnredMessages(userResolverService.UserId);
            await Clients.Caller.SendAsync("newMessageNotification", allUnreadMessagCount);
        }

        public override async Task OnConnectedAsync()
        {
            var signleUser = UserConnection.GetUserConnection(Context.ConnectionId, Context.User.Identity.Name, true);
            await securityService.ToggleUserOnlineStatus(signleUser.UserName, true);
            this.allConnectedUsers.UserConnections.RemoveAll(r => r.UserName == signleUser.UserName);
            this.allConnectedUsers.UserConnections.Add(signleUser);

            await Clients.All.SendAsync("UserConnected", signleUser, this.allConnectedUsers.UserConnections);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var singalUser = UserConnection.GetUserConnection(Context.ConnectionId, Context.User.Identity.Name, false);
            await securityService.ToggleUserOnlineStatus(singalUser.UserName, false);
            this.allConnectedUsers.UserConnections.RemoveAll(r => r.UserName == singalUser.UserName);
            await Clients.All.SendAsync("UserDisconntected", singalUser, this.allConnectedUsers.UserConnections);
            await base.OnDisconnectedAsync(exception);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }

    public class UserConnection
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public bool IsOnline { get; set; }
        protected internal static UserConnection GetUserConnection(string connectionId, string userName, bool isOnline)
        {
            return new UserConnection { ConnectionId = connectionId, UserName = userName, IsOnline = isOnline };
        }
    }

    public class AllConnectedUsers
    {
        public List<UserConnection> UserConnections = new List<UserConnection>();
    }
}
