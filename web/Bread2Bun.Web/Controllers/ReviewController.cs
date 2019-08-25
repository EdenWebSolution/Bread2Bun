using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bread2Bun.Common;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/review")]
    public class ReviewController : BaseAPIController
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewModel), 201)]
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

        [HttpPut]
        [ProducesResponseType(typeof(ReviewModel), 200)]
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

        [HttpDelete("{id:long}")]
        [ProducesResponseType(typeof(ReviewModel), 200)]

        public async Task<IActionResult> DeleteReview(long id)
        {
            try
            {
                var result = await reviewService.DeleteReviewAsync(id);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ReviewModel>), 200)]
        public async Task<IActionResult> GetAll([FromQuery]PaginationBase paginationBase)
        {
            try
            {
                var result = await reviewService.GetAll(paginationBase);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(ReviewModel), 200)]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var result = await reviewService.GetById(id);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }
    }
}