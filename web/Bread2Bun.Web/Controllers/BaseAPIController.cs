using Bread2Bun.Common.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bread2Bun.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseAPIController : ControllerBase
    {
        protected IActionResult HandleExcpetion(Exception ex)
        {
            var exType = ex.GetType().Name;

            switch (exType)
            {
                case ExceptionType.ArgumentException: return Conflict(ex.Message);
                case ExceptionType.UnauthorizedAccessException: return StatusCode(403, ex.Message);
                case ExceptionType.AuthenticationException: return Unauthorized(ex.Message);

                //*****  status code 404 range *********
                case ExceptionType.NullReferenceException:
                    return NotFound(ex.Message);
                //*****  end status code 404 range *********


                //*****  status code 500 range *********
                case ExceptionType.SqlException:
                default: return StatusCode(500, ex.Message);
                    //*****  end status code 500 range *********

            }
        }
    }
}