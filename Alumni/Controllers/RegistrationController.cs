using Alumni.DTOS;
using Alumni.Services.SignInService;
using Alumni.Services.SignUpService;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ISignUpService _signUpService;
        private readonly ISignInService _signInService;

        public RegistrationController(ISignUpService signUpService, ISignInService signInService)
        {
            _signUpService = signUpService;
            _signInService = signInService;
        }

        // SignUp API
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signUpService.SignUpAsync(userDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // SignIn API 
        [HttpPost("signin")]
        public async Task<ActionResult> SignIn([FromBody] LoginDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInService.SignInAsync(userDto);

            if (!result.IsSuccess)
            {
                if (result.ErrorCode == "INVALID_PASSWORD")
                {
                    return Unauthorized(result); // 401 HTTP Status
                }

                return BadRequest(result); // 400 HTTP Status
            }

            return Ok(result);
        }
    }
}