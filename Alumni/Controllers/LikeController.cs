using Alumni.DTOS;
using Alumni.Services.ActionService;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleLike([FromBody] LikeDTO likeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _likeService.ToggleLikeAsync(likeDto);
            if (result == "Post Not Found")
                return NotFound(new { message = result });
            return Ok(new { message = result });
        }
    }
}