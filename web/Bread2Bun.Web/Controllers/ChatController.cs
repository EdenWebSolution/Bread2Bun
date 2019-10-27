using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bread2Bun.Common;
using Bread2Bun.Common.Enum;
using Bread2Bun.Service.Chat.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : BaseAPIController
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllChatById([FromQuery]PaginationBase paginationBase, [FromQuery]long to)
        {
            var result = await chatService.GetAllChatById(paginationBase, to);
            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetChatSummary()
        {
            var result = await chatService.GetAllChatSummary();
            return Ok(result);
        }

        [HttpGet("togglemessagestatus")]
        public async Task<IActionResult> ToggleMessageReadStatus([FromQuery] long fromId, [FromQuery] MessageStatus status)
        {
            await chatService.ToggleMessageReadStatus(fromId, status);
            return Ok();
        }
    }
}