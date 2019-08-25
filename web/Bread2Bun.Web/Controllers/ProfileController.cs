using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bread2Bun.Common;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models;
using Bread2Bun.Service.Profile.Models.Review;
using Bread2Bun.Service.Profile.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/profile")]
    public class ProfileController : BaseAPIController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }
        [HttpGet("basic")]
        [ProducesResponseType(typeof(BasicInfoModel), 200)]
        public async Task<IActionResult> GetBasicProfile()
        {
            try
            {
                var result = await profileService.GetBasicInfo();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("basic/{userId:long}")]
        [ProducesResponseType(typeof(BasicInfoModel), 200)]
        public async Task<IActionResult> GetBasicProfile(long userId)
        {
            try
            {
                var result = await profileService.GetBasicInfo(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpPut("userprofile")]
        [ProducesResponseType(typeof(UserProfileModel), 200)]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileUpdateModel userProfileUpdateModel)
        {
            try
            {
                var result = await profileService.UpdateUserProfile(userProfileUpdateModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }
    }
}