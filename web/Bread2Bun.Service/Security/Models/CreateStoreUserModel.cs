using Bread2Bun.Common.Enum;
using Bread2Bun.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public class CreateStoreUserModel : BaseModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int UniversityId { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
    }
}
