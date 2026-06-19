using Alumni.DTOS;
using Alumni.DTOS.Common;
using Alumni.Models.Core;
using Alumni.Repository.SignUpRepository;
using Alumni.Services.TokenService;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Services.SignUpService
{
    public class SignUpService : ISignUpService
    {
        private readonly ISignUpRepo _signUpRepo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public SignUpService(ISignUpRepo signUpRepo, ITokenService tokenService, IMapper mapper)
        {
            _signUpRepo = signUpRepo;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ServiceResponse<AuthResponseDTO>> SignUpAsync(UserDTO userDto)
        {
            //Check email is already existed
            var existedEmail = await _signUpRepo.IsEmailExistedAsync(userDto.Email);
            if (existedEmail)
            {
                return ServiceResponse<AuthResponseDTO>.Failure("EMAIL_ALREADY_EXISTS", "Registration failed. Email is already in use.");
            }

            try
            {
                var user = _mapper.Map<User>(userDto);
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                var result = await _signUpRepo.AddUserAsync(user);

                if (result > 0)
                {
                    var authResponse = new AuthResponseDTO
                    {
                        User = _mapper.Map<UserDTO>(user),
                        Token = _tokenService.CreateToken(user)
                    };

                    return ServiceResponse<AuthResponseDTO>.Success(authResponse, "User registered successfully!");
                }

                return ServiceResponse<AuthResponseDTO>.Failure("REGISTRATION_FAILED", "Could not save user data.");
            }
            catch (DbUpdateException ex)
            {
                return ServiceResponse<AuthResponseDTO>.Failure("DATABASE_ERROR", $"Database error occurred: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return ServiceResponse<AuthResponseDTO>.Failure("SERVER_ERROR", ex.Message);
            }
        }
    }
}