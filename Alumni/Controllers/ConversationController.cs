using Alumni.DTOS.Common;
using Alumni.Services.ChatService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        // chat room api between 2 user
        [HttpPost("get-or-create/{receiverId}")]
        public async Task<IActionResult> GetOrCreateRoom(Guid receiverId)
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
            {
                return Unauthorized(ServiceResponse<object>.Failure("UNAUTHORIZED", "User invalid."));
            }

            var response = await _conversationService.GetOrCreateConversationRoomAsync(currentUserId, receiverId);

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        //  Inbox (Chat List) from my phone
        [HttpGet("my-inbox")]
        public async Task<IActionResult> GetMyConversations()
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
            {
                return Unauthorized(ServiceResponse<object>.Failure("UNAUTHORIZED", "User invalid."));
            }

            var response = await _conversationService.GetUserConversationListAsync(currentUserId);

            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}