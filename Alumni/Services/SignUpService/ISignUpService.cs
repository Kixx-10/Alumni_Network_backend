using Alumni.DTOS;
using Alumni.DTOS.Common;

namespace Alumni.Services.SignUpService
{
    public interface ISignUpService
    {
        Task<ServiceResponse<AuthResponseDTO>> SignUpAsync(UserDTO userDto);
    }
}