using Alumni.Models.DTOs;
using Alumni.Services.ProfileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("createProfile")]
        public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDTO dto)
        {
            var userIdClaim =
                User.FindFirst("id")?.Value
                ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("nameid")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid parsedUserId))
            {
                return Unauthorized(new { message = "Invalid User Token or User ID is missing." });
            }

            var response = await _profileService.CreateProfile(dto, parsedUserId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userIdClaim =
                User.FindFirst("id")?.Value
                ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("nameid")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid parsedUserId))
            {
                return Unauthorized(new { message = "Invalid User Token or User ID is missing." });
            }

            var response = await _profileService.GetMyProfile(parsedUserId);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetProfileById(Guid profileId)
        {
            var response = await _profileService.GetProfileById(profileId);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }
    }
}
