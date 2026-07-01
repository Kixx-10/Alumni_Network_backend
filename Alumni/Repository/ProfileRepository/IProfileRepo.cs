using Alumni.Models.Core;

namespace Alumni.Repository.ProfileRepository
{
    public interface IProfileRepo
    {
        Task<int> CreateProfileRepository(Profile profile);
        Task<int> UpdateProfileRepository(Profile profile);
        Task<Profile?> GetProfileByIdRepository(Guid profileId);
        Task<Profile?> GetProfileByUserIdRepository(Guid userId);
    }
}
