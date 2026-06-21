using Alumni.DTOS;
using Alumni.Models.Core;
using Alumni.Services.PostServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alumni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("CreatePost")]
        [Authorize]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostDTO createPostDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("id")?.Value
                              ?? User.FindFirst("userId")?.Value;

            var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value
                                ?? User.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userRoleClaim))
            {
                return Unauthorized(new { message = "Invalid user token configuration." });
            }

            var userId = Guid.Parse(userIdClaim);

            if (!Enum.TryParse<UserRole>(userRoleClaim, true, out UserRole userRole))
            {
                return StatusCode(403, new { message = "Invalid user role assigned to this token." });
            }

            var result = await _postService.CreatePostService(createPostDto, userId, userRole);

            if (!result.IsSuccess)
            {
                return StatusCode(403, result);
            }

            return Ok(result);
        }

        [HttpGet("FetchAllPosts")]
        public async Task<ActionResult> GetAllPosts()
        {
            var result = await _postService.GetAllPostService();
            return Ok(result);
        }
    }
}