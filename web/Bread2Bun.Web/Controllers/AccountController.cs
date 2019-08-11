using Bread2Bun.Service.ExceptionHandler;
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
                var result = await securityService.CreateUser(createStoreUserModel);
                return Created("", result);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return Ok("success post login");
        }
    }
}