using Alumni.DTOs;
using Alumni.Models.Master;

namespace Alumni.Profiles
{
    public class MessageMapper : AutoMapper.Profile
    {
        public MessageMapper()
        {
            // Create Message
            CreateMap<MessageCreateDTO, Message>()
                .ForMember(dest => dest.MessageId, opt => opt.Ignore())
                .ForMember(dest => dest.Conversation, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.Ignore())
                .ForMember(dest => dest.Receiver, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());

            // Read Message
            CreateMap<Message, MessageReadDTO>();
        }
    }
}
