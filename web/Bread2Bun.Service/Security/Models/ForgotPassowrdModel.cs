using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public class ForgotPassowrdModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
