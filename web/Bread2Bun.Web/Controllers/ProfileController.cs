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


        [HttpPost("review")]
        public async Task<IActionResult> CreateNewReview([FromBody]ReviewCreateModel reviewCreateModel)
        {
            try
            {
                var result = await reviewService.AddReviewAsync(reviewCreateModel);
                return Created(string.Empty, result);
            }

            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpPut("review")]
        public async Task<IActionResult> UpdateReview([FromBody]ReviewUpdateModel reviewUpdateModel)
        {
            try
            {
                var result = await reviewService.UpdateReviewAsync(reviewUpdateModel);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }
    }
}