using Bread2Bun.Common.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bread2Bun.Web.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseAPIController : ControllerBase
    {
        protected IActionResult HandleExcpetion(Exception ex)
        {
            var exType = ex.GetType().Name;

            switch (exType)
            {
                case ExceptionType.ArgumentException: return Conflict(ex.Message);
                case ExceptionType.UnauthorizedAccessException: return Unauthorized(ex.Message);

                default: return BadRequest(ex.Message);
            }
        }
    }
}