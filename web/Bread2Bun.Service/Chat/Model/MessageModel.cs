using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Chat.Model
{
    public class MessageModel
    {
        public long ToId { get; set; }
        public string Text { get; set; }
        public string ClientUniqueId { get; set; }
    }
}
