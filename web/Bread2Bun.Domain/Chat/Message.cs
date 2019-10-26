using Bread2Bun.Domain.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bread2Bun.Domain.Chat
{
    public class Message : Audit
    {
        public long Id { get; protected set; }
        public long FromId { get; protected set; }
        [ForeignKey(nameof(FromId))]
        public StoreUser From { get; protected set; }
        public long ToId { get; protected set; }
        [ForeignKey(nameof(ToId))]
        public StoreUser To { get; protected set; }
        public string Text { get; protected set; }
        public bool IsRead { get; protected set; }

        public Guid ThreadId { get; set; }
        [ForeignKey(nameof(ThreadId))]
        public MessageThreaad MessageThreaad { get; set; }

        public Message Create(long fromId, long toId, string text)
        {
            FromId = fromId;
            ToId = toId;
            Text = text;
            return this;
        }

    }
}
