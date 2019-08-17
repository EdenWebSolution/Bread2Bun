using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
