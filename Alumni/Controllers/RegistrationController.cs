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
        //SignUp
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] UserDTO userDto)
        {
            //var validationResult = await validator.ValidateAsync(userDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _signUpService.SignUpAsync(userDto);

            if (result == null)
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Registration failed. Email might already be in use."
                });
            }
            return Ok(new
            {
                token = result.Token,
                status = true,
                message = "User registered successfully!",
                data = result.User,
            });
        }

        //SignIn
        [HttpPost("signin")]
        public async Task<ActionResult> SignIn([FromBody] LoginDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _signInService.SignInAsync(userDto);
            if (result == null)
            {
                return BadRequest(new
                {
                    message = "Login Failed"
                });
            }
            return Ok(new
            {
                token = result.Token,
                status = true,
                message = $"{result.User.Name}: Login successfully!",
            });

        }
    }
}