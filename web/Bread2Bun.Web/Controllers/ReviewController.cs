using Bread2Bun.Common;
using Bread2Bun.Common.Constants;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models.Review;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/review")]
    public class ReviewController : BaseAPIController
    {
        private readonly IReviewService reviewService;
        private readonly IHostingEnvironment hostingEnvironment;

        public ReviewController(IReviewService reviewService, IHostingEnvironment hostingEnvironment)
        {
            this.reviewService = reviewService;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewModel), 201)]
        public async Task<IActionResult> CreateNewReview()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var reviewModel = httpRequest.Form["entityData"];
                var item = JsonConvert.DeserializeObject<ReviewCreateModel>(reviewModel);
                var result = await SaveImage(FolderPath.Review);
                if (result.FileIsThere)
                {
                    item.ReviewImage = result.FileNames[0];
                }
                var res =  await reviewService.AddReviewAsync(item);
                return Created(string.Empty, res);
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

        [HttpGet]
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

        [HttpGet("list/{userId:long}")]
        [ProducesResponseType(typeof(IEnumerable<ReviewModel>), 200)]
        public async Task<IActionResult> GetAll([FromQuery]PaginationBase paginationBase, long userId)
        {
            try
            {
                var result = await reviewService.GetAll(paginationBase, userId);
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<FileSaveResult> SaveImage(string folderName)
        {
            try
            {
                var fileSaveResult = new FileSaveResult();
                if (HttpContext.Request.Form.Files == null || HttpContext.Request.Form.Files.Count == 0) return fileSaveResult;

                var files = HttpContext.Request.Form.Files;
                var filenames = new List<string>();
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(this.hostingEnvironment.WebRootPath, folderName);
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                filenames.Add(fileName);
                                fileSaveResult.FileIsThere = true;
                            }
                        }
                    }
                }
                fileSaveResult.FileNames = filenames;
                return fileSaveResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}