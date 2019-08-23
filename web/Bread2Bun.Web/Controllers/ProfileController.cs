using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models.Review;
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
        public async Task<IActionResult> GetBasicProfile()
        {
            var result = await profileService.GetBasicInfo();
            return Ok(result);
        }


        [HttpPost("review")]
        public async Task<IActionResult> CreateNewReview([FromBody]ReviewCreateModel reviewCreateModel)
        {
            var result = await reviewService.AddReviewAsync(reviewCreateModel);
            return Ok(result);
        }
    }
}