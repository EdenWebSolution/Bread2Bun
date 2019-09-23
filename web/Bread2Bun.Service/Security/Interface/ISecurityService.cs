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
        Task<StoreUserModel> CreateUserAsync(CreateStoreUserModel createStoreUserModel);
        Task<AuthTokenModel> LoginAsync(LoginModel loginModel);
        Task ForgotPasswordAsync(ForgotPassowrdModel forgotPassowrdModel);
        Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
        Task ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);
        Task AddNewClaim();
        Task Logout();
    }
}
