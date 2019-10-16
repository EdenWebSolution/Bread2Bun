using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Chat.Model
{
    public class ChatSummaryModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public string LastMessage { get; set; }
    }
}
