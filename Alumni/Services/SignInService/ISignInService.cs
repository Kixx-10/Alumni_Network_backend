using Alumni.DTOS;
using Alumni.DTOS.Common;

namespace Alumni.Services.SignInService
{
    public interface ISignInService
    {
        Task<ServiceResponse<AuthResponseDTO>> SignInAsync(LoginDTO userDto);
    }
}