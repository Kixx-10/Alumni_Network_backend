using Alumni.DTOS.Common;
using Alumni.Services.ChatService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpGet("history/{conversationId}")]
        public async Task<IActionResult> GetChatHistory(Guid conversationId, [FromQuery] int limit = 50)
        {
            var response = await _messageService.GetChatHistoryAsync(conversationId, limit);

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("read/{messageId}")]
        public async Task<IActionResult> MarkAsRead(Guid messageId)
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
            {
                return Unauthorized(ServiceResponse<object>.Failure("UNAUTHORIZED", "User invalid."));
            }

            var response = await _messageService.ReadMessageAsync(messageId, currentUserId);

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        //for swagger test only
        //[HttpPost("send-test")]
        //public async Task<IActionResult> SendTestMessage([FromBody] MessageCreateDTO dto)
        //{
        //    var userIdClaim = User.FindFirst("id")?.Value;
        //    if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
        //    {
        //        return Unauthorized();
        //    }


        //    var response = await _messageService.SendMessageAsync(dto, currentUserId);

        //    if (!response.IsSuccess) return BadRequest(response);
        //    return Ok(response);
        //}
    }
}