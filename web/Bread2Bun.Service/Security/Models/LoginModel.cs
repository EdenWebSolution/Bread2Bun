using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public sealed class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
