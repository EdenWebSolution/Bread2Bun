using Bread2Bun.Common.Model;
using Bread2Bun.Service.Security.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Security.Interface
{
    public interface ISecurityService
    {
        Task<StoreUserModel> CreateUser(CreateStoreUserModel createStoreUserModel);
        Task Login(LoginModel loginModel);
    }
}
