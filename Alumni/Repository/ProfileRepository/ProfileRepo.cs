using Alumni.Data;
using Alumni.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.ProfileRepository
{
    public class ProfileRepo : IProfileRepo
    {
        private readonly AppDbContext _context;
        public ProfileRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProfileRepository(Profile profile)
        {
            _context.Profiles.Add(profile);
            return await _context.SaveChangesAsync();
        }

        //To see or get My Profile
        public async Task<Profile?> GetProfileByUserIdRepository(Guid userId)
        {
            return await _context.Profiles.SingleOrDefaultAsync(p => p.UserId == userId);
        }

        //To see Other User's Profile
        public async Task<Profile?> GetProfileByIdRepository(Guid profileId)
        {
            return await _context.Profiles.SingleOrDefaultAsync(p => p.ProfileId == profileId);
        }

        public async Task<int> UpdateProfileRepository(Profile profile)
        {
            var existingProfile = await _context.Profiles.SingleOrDefaultAsync(
                p => p.ProfileId == profile.ProfileId);
            if (existingProfile == null) return 0;
            existingProfile.FullName = profile.FullName;
            existingProfile.Class = profile.Class;
            existingProfile.AvatarUrl = profile.AvatarUrl;
            existingProfile.GraduationYear = profile.GraduationYear;
            existingProfile.Department = profile.Department;
            existingProfile.University = profile.University;
            existingProfile.Company = profile.Company;
            existingProfile.JobTitle = profile.JobTitle;
            existingProfile.UpdatedDate = DateTime.UtcNow.Date;
            return await _context.SaveChangesAsync();
        }
    }
}