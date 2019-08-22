using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Domain.Security
{
    public class Claims : Audit
    {
        public virtual int Id { get; set; }
        public virtual string Claim { get; set; }
    }
}
