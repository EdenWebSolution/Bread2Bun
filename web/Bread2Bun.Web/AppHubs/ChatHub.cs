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

        public ChatHub(IChatService chatService)
        {
            this.chatService = chatService;
        }

        //public async Task SendMessageToAll(MessageModel msg)
        //{
        //    //var chat = await chatService.SendMessage(msg);
        //    //await Clients.All.SendAsync("ReceiveMessage", chat);
        //}

        public async Task SendMessage(MessageModel msg)
        {
            var chat = await chatService.SendMessage(msg);
            await Clients.Client(msg.ClientUniqueId).SendAsync("ReceiveMessage", chat);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UserConnected", UserConnection.GetUserConnection(Context.ConnectionId, Context.User.Identity.Name, true));
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UserDisconntected", UserConnection.GetUserConnection(Context.ConnectionId, Context.User.Identity.Name, false));
            await base.OnDisconnectedAsync(exception);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }

    internal class UserConnection
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public bool IsOnline { get; set; }
        protected internal static UserConnection GetUserConnection(string connectionId, string userName, bool isOnline)
        {
            return new UserConnection { ConnectionId = connectionId, UserName = userName, IsOnline = isOnline };
        }
    }
}
