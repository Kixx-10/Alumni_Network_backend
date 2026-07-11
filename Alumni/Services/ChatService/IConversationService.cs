using Alumni.DTOs;
using Alumni.DTOS.Common;

namespace Alumni.Services.ChatService
{
    public interface IConversationService
    {
        // Serach chat room or conversation between two users, if not found, create a new conversation room
        Task<ServiceResponse<ConversationReadDTO>> GetOrCreateConversationRoomAsync(Guid user1Id, Guid user2Id);

        //the chat list that user current participate in, including the last message and the other user information
        Task<ServiceResponse<IEnumerable<ConversationReadDTO>>> GetUserConversationListAsync(Guid userId);
    }
}

