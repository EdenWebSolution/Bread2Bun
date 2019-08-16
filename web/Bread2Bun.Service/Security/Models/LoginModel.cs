using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public sealed class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
