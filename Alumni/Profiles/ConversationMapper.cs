using Alumni.DTOs;
using Alumni.Models.Core;
using Alumni.Models.Master;

namespace Alumni.Profiles
{
    public class ConversationMapper : AutoMapper.Profile
    {
        public ConversationMapper()
        {
            // Read Conversation
            CreateMap<Conversation, ConversationReadDTO>()

                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var currentUserId = (Guid)context.Items["CurrentUserId"];
                    // လက်ရှိ user က User1 ဖြစ်နေရင် တစ်ဖက်လူက User2 ၊ မဟုတ်ရင် User1
                    User otherUser = src.User1Id == currentUserId ? src.User2 : src.User1;

                    // User ရဲ့ Profile ရှိရင် Profile က နာမည်ယူမယ်၊ မရှိရင် User ရဲ့ အကောင့်နာမည် ယူမယ်
                    return otherUser.Profile?.FullName ?? otherUser.Name;
                }))

                // Recipient Avatar အတွက် Logic
                .ForMember(dest => dest.RecipientAvatar, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var currentUserId = (Guid)context.Items["CurrentUserId"];
                    User otherUser = src.User1Id == currentUserId ? src.User2 : src.User1;

                    return otherUser.Profile?.AvatarUrl; // ပုံရှိရင် ပုံ Url ထွက်လာမယ်၊ မရှိရင် null ဖြစ်မယ်
                }));
        }
    }
}