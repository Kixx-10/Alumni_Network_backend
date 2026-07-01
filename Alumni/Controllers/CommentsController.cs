using Alumni.Models.DTOs;
using Alumni.Services.CommentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost("api/comments")]
        public async Task<IActionResult> CreateComment([FromBody] WriteCommentDTO dto)
        {
            var userIdClaim =
                User.FindFirst("id")?.Value                                                  // JWT payload key
                ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value   // fallback
                ?? User.FindFirst("nameid")?.Value;                                          // fallback

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid parsedUserId))
            {
                return Unauthorized(new { message = "Invalid User Token or User ID is missing." });
            }

            var response = await _commentService.CreateCommentAsync(dto, parsedUserId);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(
                nameof(GetPostComments),
                new { postId = dto.PostId },
                response
            );
        }
        [HttpGet("api/posts/{postId}/comments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostComments(Guid postId)
        {
            var response = await _commentService.GetPostCommentsAsync(postId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
