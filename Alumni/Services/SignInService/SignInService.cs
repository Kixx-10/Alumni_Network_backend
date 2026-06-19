using Alumni.DTOS;
using Alumni.Repository.SignInRepository;
using Alumni.Services.TokenService;
using AutoMapper;

namespace Alumni.Services.SignInService
{
    public class SignInService : ISignInService
    {
        private readonly ISignInRepo _signRepo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public SignInService(ISignInRepo signRepo, ITokenService tokenService, IMapper mapper)
        {
            _signRepo = signRepo;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO?> SignInAsync(LoginDTO userDto)
        {

            var dbUser = await _signRepo.GetUserByEmailAsync(userDto.Email);

            if (dbUser == null)
            {
                return null;
            }


            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userDto.Password, dbUser.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            return new AuthResponseDTO
            {
                User = _mapper.Map<UserDTO>(dbUser),
                Token = _tokenService.CreateToken(dbUser)
            };
        }
    }
}