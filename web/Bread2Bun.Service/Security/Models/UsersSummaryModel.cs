using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Security.Models
{
    public class UsersSummaryModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string ProfileImagePath { get; set; }
    }
}
