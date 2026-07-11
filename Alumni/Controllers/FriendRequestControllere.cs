using Alumni.DTOS;
using Alumni.Services.FriendService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendRequestControllere : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;
        public FriendRequestControllere(IFriendRequestService friendRequestService)
        {
            _friendRequestService = friendRequestService;
        }
        private Guid GetCurrentUserId()
        {
            var userIdString = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            return Guid.Parse(userIdString);
        }
        [HttpPost("send_friendRequest")]
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestCreateDTO createDto)
        {
            try
            {
                var senderId = GetCurrentUserId();
                var result = await _friendRequestService.SendFriendRequestAsync(senderId, createDto);

                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { IsSuccess = false, Message = ex.Message });
            }
        }
        [HttpPut("accept/{requestId}")]
        public async Task<IActionResult> AcceptFriendRequest(Guid requestId)
        {
            try
            {
                var result = await _friendRequestService.AcceptFriendRequestAsync(requestId);

                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { IsSuccess = false, Message = ex.Message });
            }
        }
        [HttpPut("reject/{requestId}")]
        public async Task<IActionResult> RejectFriendRequest(Guid requestId)
        {
            try
            {
                var result = await _friendRequestService.RejectFriendRequestAsync(requestId);

                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { IsSuccess = false, Message = ex.Message });
            }
        }
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequests()
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _friendRequestService.GetPendingRequestsAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}


