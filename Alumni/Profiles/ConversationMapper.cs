using Alumni.DTOs;
using Alumni.Models.Master;

namespace Alumni.Profiles
{
    public class ConversationMapper : AutoMapper.Profile
    {
        public ConversationMapper()
        {
            // Create Conversation
            CreateMap<ConversationCreateDTO, Conversation>()
                .ForMember(dest => dest.ConversationId, opt => opt.Ignore())
                .ForMember(dest => dest.LastMessageId, opt => opt.Ignore())
                .ForMember(dest => dest.LastMessage, opt => opt.Ignore())
                .ForMember(dest => dest.Messages, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());

            // Read Conversation
            CreateMap<Conversation, ConversationReadDTO>()
                .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.LastMessage));
        }
    }
}
