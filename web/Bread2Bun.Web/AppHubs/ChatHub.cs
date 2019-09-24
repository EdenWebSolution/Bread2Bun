﻿using Bread2Bun.Service.Chat.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bread2Bun.Web.AppHubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(MessageModel message)
        {
            await Clients.All.SendAsync("SendMessage", message);
        }

        public async Task SendPrivateMessage()
        {

        }
    }
}