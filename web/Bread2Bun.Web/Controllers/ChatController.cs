using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [AllowAnonymous]
    public class ChatController : BaseAPIController
    {
        [HttpPost("message")]
        public async Task<IActionResult> Chat()
        {
            return Ok();
        }
    }
}