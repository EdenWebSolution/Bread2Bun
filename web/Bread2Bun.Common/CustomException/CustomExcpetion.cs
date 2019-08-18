using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Common.CustomException
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string message) : base(message) { }
    }
}
