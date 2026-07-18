using Alumni.DTOS;
using Alumni.Models.Core;

namespace Alumni.Profiles
{
    public class UserMapper : AutoMapper.Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserDiscoverDTO>()
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Profile != null ? src.Profile.AvatarUrl : string.Empty));
        }
    }
}