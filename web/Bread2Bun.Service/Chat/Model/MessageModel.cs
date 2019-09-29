using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Chat.Model
{
    public class MessageModel
    {
        public long Id { get; set; }
        public long ToId { get; set; }
        public string Text { get; set; }
        public long FromId { get; set; }
        public string ClientUniqueId { get; set; }
    }
}
