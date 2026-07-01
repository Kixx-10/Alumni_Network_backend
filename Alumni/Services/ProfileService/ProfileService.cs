
using Alumni.DTOS.Common;
using Alumni.Models.DTOs;
using Alumni.Repository.ProfileRepository;
using AutoMapper;

namespace Alumni.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepo _profileRepo;

        public ProfileService(IMapper mapper, IProfileRepo profileRepo)
        {
            _mapper = mapper;
            _profileRepo = profileRepo;
        }
        //create new profile
        public async Task<ServiceResponse<ResponseProfileDTO>> CreateProfile(CreateProfileDTO dto, Guid userId)
        {
            var response = new ServiceResponse<ResponseProfileDTO>();
            try
            {
                var existingProfile = await _profileRepo.GetProfileByUserIdRepository(userId);
                if (existingProfile != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Profile already exists for this user.";
                    return response;
                }

                var newProfile = _mapper.Map<Models.Core.Profile>(dto);
                newProfile.UserId = userId;
                newProfile.CreatedDate = DateTime.UtcNow;

                var result = await _profileRepo.CreateProfileRepository(newProfile);
                if (result > 0)
                {
                    response.Data = _mapper.Map<ResponseProfileDTO>(newProfile);
                    response.IsSuccess = true;
                    response.Message = "Successfully created your profile.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong while creating profile.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        // ── GetMyProfile 
        public async Task<ServiceResponse<ResponseProfileDTO>> GetMyProfile(Guid userId)
        {
            var response = new ServiceResponse<ResponseProfileDTO>();
            try
            {
                Console.WriteLine($"=====================> GetMyProfile for UserId: {userId}");

                var profile = await _profileRepo.GetProfileByUserIdRepository(userId);
                if (profile == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Profile not found. Please complete your profile setup.";
                    return response;
                }

                response.Data = _mapper.Map<ResponseProfileDTO>(profile);
                response.IsSuccess = true;
                response.Message = "Profile fetched successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        // ── GetProfileById — တစ်ဦးချင်း profile ကြည့်ခြင်း 
        public async Task<ServiceResponse<ResponseProfileDTO>> GetProfileById(Guid profileId)
        {
            var response = new ServiceResponse<ResponseProfileDTO>();
            try
            {
                var profile = await _profileRepo.GetProfileByIdRepository(profileId);
                if (profile == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Profile not found.";
                    return response;
                }

                response.Data = _mapper.Map<ResponseProfileDTO>(profile);
                response.IsSuccess = true;
                response.Message = "Profile fetched successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public Task<ServiceResponse<ResponseProfileDTO>> UpdateProfile(UpdateProfileDTO dto, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
