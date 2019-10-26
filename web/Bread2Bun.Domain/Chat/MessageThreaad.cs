using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Domain.Chat
{
    public class MessageThreaad
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string ChatGroup { get; set; }

    }
}
