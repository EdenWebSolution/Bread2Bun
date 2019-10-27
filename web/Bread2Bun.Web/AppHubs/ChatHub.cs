using Bread2Bun.Service.Chat.Interface;
using Bread2Bun.Service.Chat.Model;
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


        public ChatHub(IChatService chatService, AllConnectedUsers allConnectedUsers)
        {
            this.chatService = chatService;
            this.allConnectedUsers = allConnectedUsers;
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
                await Clients.Client(msg.ClientUniqueId).SendAsync("ReceiveMessage", chat);
        }

        public override async Task OnConnectedAsync()
        {
            var signleUser = UserConnection.GetUserConnection(Context.ConnectionId, Context.User.Identity.Name, true);
            this.allConnectedUsers.UserConnections.Remove(this.allConnectedUsers.UserConnections.FirstOrDefault(f => f.UserName == signleUser.UserName));
            this.allConnectedUsers.UserConnections.Add(signleUser);

            await Clients.All.SendAsync("UserConnected", signleUser, this.allConnectedUsers.UserConnections);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var singalUser = UserConnection.GetUserConnection(Context.ConnectionId, Context.User.Identity.Name, false);
            this.allConnectedUsers.UserConnections.Remove(singalUser);
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
