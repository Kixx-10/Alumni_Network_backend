using Alumni.DTOS;

namespace Alumni.Services.SignInService
{
    public interface ISignInService
    {
        Task<AuthResponseDTO?> SignInAsync(LoginDTO userDto);
    }
}