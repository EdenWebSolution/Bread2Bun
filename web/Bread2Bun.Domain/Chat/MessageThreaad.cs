using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Domain.Chat
{
    public class MessageThreaad
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string ChatGroup { get; protected set; }

        public MessageThreaad Create(string userGroup)
        {
            ChatGroup = userGroup;
            return this;
        }

    }
}
