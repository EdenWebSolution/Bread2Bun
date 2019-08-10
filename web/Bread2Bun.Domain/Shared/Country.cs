using Bread2Bun.Domain.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Domain.Shared
{
    public class Country : Audit
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; }
    }
}
