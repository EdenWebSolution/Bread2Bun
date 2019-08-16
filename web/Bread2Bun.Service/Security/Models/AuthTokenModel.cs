using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public class AuthTokenModel
    {
        public string Token { get; set; }
        public dynamic ExtensionData { get; set; }
    }
}
