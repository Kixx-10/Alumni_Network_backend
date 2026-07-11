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
                .ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.Sender.Email));
        }
    }
}
