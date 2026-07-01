using Alumni.DTOS.Common;
using Alumni.Models.DTOs;

namespace Alumni.Services.ProfileService
{
    public interface IProfileService
    {
        Task<ServiceResponse<ResponseProfileDTO>> CreateProfile(CreateProfileDTO dto, Guid userId);

        Task<ServiceResponse<ResponseProfileDTO>> GetMyProfile(Guid userId);

        Task<ServiceResponse<ResponseProfileDTO>> GetProfileById(Guid profileId);
        Task<ServiceResponse<ResponseProfileDTO>> UpdateProfile(UpdateProfileDTO dto, Guid userId);
    }
}