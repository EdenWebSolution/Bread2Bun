using Bread2Bun.Domain.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bread2Bun.Domain.Chat
{
    public class Message : Audit
    {
        public long Id { get; set; }
        public long FromId { get; set; }
        [ForeignKey(nameof(FromId))]
        public StoreUser From { get; set; }
        public long ToId { get; set; }
        [ForeignKey(nameof(ToId))]
        public StoreUser To { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }

    }
}
