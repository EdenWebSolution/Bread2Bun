using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public class ConfirmEmailModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
