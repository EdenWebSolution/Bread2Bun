using Bread2Bun.Service.Profile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile.Interface
{
    public interface IProfileService
    {
        Task<BasicInfoModel> GetBasicInfo();
    }
}
