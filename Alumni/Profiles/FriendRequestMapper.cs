using Alumni.DTOS;
using Alumni.Models.Master;

namespace Alumni.Profiles
{
    public class FriendRequestMapper : AutoMapper.Profile
    {
        public FriendRequestMapper()
        {
            CreateMap<FriendRequestCreateDTO, FriendRequest>();


            CreateMap<FriendRequest, FriendRequestResponseDTO>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Name))
                .ForMember(dest => dest.SenderAvatarUrl, opt => opt.MapFrom(src =>
                    src.Sender.Profile != null ? src.Sender.Profile.AvatarUrl : string.Empty));
        }
    }
}
