using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Chat.Model
{
    public class ChatModel
    {
        public long ToId { get; set; }
        public long FromId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Date { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
    }
}
