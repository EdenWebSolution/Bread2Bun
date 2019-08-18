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

        public AccountController(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        [HttpPost("user/new")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateStoreUserModel createStoreUserModel)
        {
            try
            {
                var result = await securityService.CreateUserAsync(createStoreUserModel);
                return Created("", result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpPost("user/confirmemail")]
        [AllowAnonymous]
        public async Task<IActionResult> Confirmemail([FromBody] ConfirmEmailModel confirmEmailModel)
        {
            try
            {
                await securityService.ConfirmEmailAsync(confirmEmailModel);
                return Ok();
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

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}