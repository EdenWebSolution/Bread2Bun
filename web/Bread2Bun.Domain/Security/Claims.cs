using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Domain.Security
{
    public class Claims : Audit
    {
        public int Id { get; set; }
        public string Claim { get; set; }
    }
}
