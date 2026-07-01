using Alumni.Models.Core;
using Alumni.Models.DTOs;

namespace Alumni.Profiles
{
    public class ProfileMapper : AutoMapper.Profile
    {
        public ProfileMapper()
        {
            CreateMap<CreateProfileDTO, Profile>()
                .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Profile, ResponseProfileDTO>();
            CreateMap<UpdateProfileDTO, Profile>();
        }
    }
}