﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bread2Bun.Common.Constants;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models;
using Bread2Bun.Service.Profile.Models.UserProfile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using static System.Net.Mime.MediaTypeNames;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/profile")]
    public class ProfileController : BaseAPIController
    {
        private readonly IProfileService profileService;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProfileController(IProfileService profileService, IHostingEnvironment hostingEnvironment)
        {
            this.profileService = profileService;
            this.hostingEnvironment = hostingEnvironment;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [ProducesErrorResponseType(typeof(void))]

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

        [HttpPut("userprofile/profileimage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateUserProfileImage()
        {
            try
            {
                var result = SaveImage(FolderPath.ProfileImages);
                if (result.FileIsThere)
                {
                    await profileService.UpdateUserProfileImage(UserId, result.FileNames[0]);
                }
                return Ok();
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

        [HttpGet("userprofile")]
        [ProducesResponseType(typeof(UserProfileUpdateModel), 200)]
        public async Task<IActionResult> GetUserProfileInfo()
        {
            try
            {
                var result = await profileService.GetUserProfileInfo(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("userprofile/{username}")]
        public async Task<IActionResult> GetUserProfileInfoByUserName(string username)
        {
            try
            {
                var result = await profileService.GetUserProfileInfoByUserName(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("userprofile/status")]
        public async Task<IActionResult> GetUserProfileStatus()
        {
            try
            {
                var result = await profileService.GetUserProfileStatus(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }


        [HttpGet("userprofile/userprofileviewinfo")]
        [ProducesResponseType(typeof(UserProfileViewResult), 200)]
        public async Task<IActionResult> GetUserProfileViewInfo()
        {
            try
            {
                var result = await profileService.GetUserProfileViewInfo(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("userprofile/feeds")]
        public async Task<IActionResult> GetFeeds(int skip, int take)
        {
            try
            {
                var result = await profileService.GetFeeds(UserId, skip, take);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private FileSaveResult SaveImage(string folderName)
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
                        var imageStream = Image.OpenReadStream();

                        var uploads = Path.Combine(this.hostingEnvironment.WebRootPath, folderName);
                        if (Image.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Image.FileName);
                            using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(imageStream))
                            {
                                var height = (int)Math.Round(0.5 * image.Height);
                                var width = (int)Math.Round(0.5 * image.Width);

                                image.Mutate(x => x.Resize(width, height));
                                image.Save($"{uploads}/{fileName}"); // Automatic encoder selected based on extension.
                            }
                            filenames.Add(fileName);
                            fileSaveResult.FileIsThere = true;
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