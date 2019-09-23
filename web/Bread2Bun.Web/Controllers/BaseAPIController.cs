using Bread2Bun.Common.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
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

        protected int UserId
        {
            get
            {
                try
                {
                    var res = int.TryParse(GetClaims().First(c => c.Type == "sub").Value, out var userid);
                    return userid;
                }
                catch
                {
                    throw new Exception("UserId not founded");
                }
            }
        }

        private List<Claim> GetClaims()
        {
            try
            {
                var tokenString = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
                var token = new JwtSecurityToken(jwtEncodedString: tokenString);
                return token.Claims.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("tenant not founded");
            }
        }
    }
}