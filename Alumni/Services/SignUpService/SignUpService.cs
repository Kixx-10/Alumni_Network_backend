using Alumni.DTOS;
using Alumni.Models.Core;
using Alumni.Repository.SignUpRepository; // Repository အား ချိတ်ဆက်ခြင်း
using Alumni.Services.TokenService;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Services.SignUpService
{
    public class SignUpService : ISignUpService
    {
        private readonly ISignUpRepo _signUpRepo; // Context အစား ပြောင်းလဲခြင်း
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public SignUpService(ISignUpRepo signUpRepo, ITokenService tokenService, IMapper mapper)
        {
            _signUpRepo = signUpRepo;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO?> SignUpAsync(UserDTO userDto)
        {
            // Email ရှိမရှိကို Repository ကတစ်ဆင့် စစ်ဆေးခြင်း
            var existedEmail = await _signUpRepo.IsEmailExistedAsync(userDto.Email);
            if (existedEmail)
            {
                return null;
            }

            try
            {
                var user = _mapper.Map<User>(userDto);
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                // User အသစ်အား သိမ်းဆည်းရန် Repository သို့ ပို့ခြင်း
                var result = await _signUpRepo.AddUserAsync(user);

                if (result > 0)
                {
                    return new AuthResponseDTO
                    {
                        User = _mapper.Map<UserDTO>(user),
                        Token = _tokenService.CreateToken(user)
                    };
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}