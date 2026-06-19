using Alumni.DTOS;

namespace Alumni.Services.SignUpService
{
    public interface ISignUpService
    {
        Task<AuthResponseDTO?> SignUpAsync(UserDTO userDto);
    }
}