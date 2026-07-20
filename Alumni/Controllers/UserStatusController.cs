using Alumni.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserStatusController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserStatusController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}/status")]
        public async Task<IActionResult> GetUserStatus(Guid userId)
        {
            var (isOnline, lastSeen) = await _userService.GetUserStatusAsync(userId);
            return Ok(new { isOnline, lastSeen });
        }
    }
}