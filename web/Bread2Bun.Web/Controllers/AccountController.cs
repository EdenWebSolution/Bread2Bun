using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/security")]
    public class AccountController : BaseAPIController
    {
        [HttpGet("noauth")]
        [AllowAnonymous]
        public IActionResult TestGet()
        {
            throw new System.Exception("labba");
            //return Ok("success get API at \"no auth test\"");
        }

        [HttpGet("withauth")]
        public IActionResult TestGetWithAuth()
        {
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return Ok("success post login");
        }
    }
}