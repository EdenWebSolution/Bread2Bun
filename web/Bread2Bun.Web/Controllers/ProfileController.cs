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
        private readonly IReviewService reviewService;

        public ProfileController(IProfileService profileService, IReviewService reviewService)
        {
            this.profileService = profileService;
            this.reviewService = reviewService;
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


        [HttpPost("userprofile")]
        [ProducesResponseType(typeof(UserProfileModel), 201)]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreateModel userProfileCreateModel)
        {
            try
            {
                var result = await profileService.CreateUserProfile(userProfileCreateModel);
                return Created(string.Empty, result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }
    }
}