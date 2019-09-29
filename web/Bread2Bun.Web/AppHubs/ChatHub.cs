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
        public Task SendMessage(MessageModel msg)
        {
            chatService.SendMessage(msg);
            return Clients.All.SendAsync("ReceiveMessage", msg);
        }

        public async override Task OnConnectedAsync()
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UserConnected", Context.ConnectionId, Context.User.Identity.Name);
            await base.OnConnectedAsync();
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
