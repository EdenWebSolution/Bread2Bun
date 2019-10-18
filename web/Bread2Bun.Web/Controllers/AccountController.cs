using Bread2Bun.Common;
using Bread2Bun.Common.Model;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Security.Interface;
using Bread2Bun.Service.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/security")]
    public class AccountController : BaseAPIController
    {
        private readonly ISecurityService securityService;
        private readonly IProfileService profileService;

        public AccountController(ISecurityService securityService, IProfileService profileService)
        {
            this.securityService = securityService;
            this.profileService = profileService;
        }

        [HttpPost("user/new")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(StoreUserModel), 201)]
        public async Task<IActionResult> Create([FromBody] CreateStoreUserModel createStoreUserModel)
        {
            try
            {
                var result = await securityService.CreateUserAsync(createStoreUserModel);
                await profileService.CreateUserProfile(result.Id);
                return Created("", result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("user/confirmemail")]
        [AllowAnonymous]
        public async Task<IActionResult> Confirmemail([FromQuery] ConfirmEmailModel confirmEmailModel)
        {
            try
            {
                await securityService.ConfirmEmailAsync(confirmEmailModel);

                return Redirect(GlobalConfig.PresentationBaseUrl);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            try
            {
                var token = await securityService.LoginAsync(loginModel);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await securityService.Logout();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }


        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPasswordResetEmail([FromBody]ForgotPassowrdModel forgotPassowrdModel)
        {
            try
            {
                await securityService.ForgotPasswordAsync(forgotPassowrdModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel resetPasswordModel)
        {
            try
            {
                await securityService.ResetPasswordAsync(resetPasswordModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpPost("claims/add")]
        public async Task<IActionResult> AddClaim()
        {
            try
            {
                await securityService.AddNewClaim();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }
    }
}