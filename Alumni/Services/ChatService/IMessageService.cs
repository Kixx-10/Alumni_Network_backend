using Alumni.DTOs;
using Alumni.DTOS.Common;

namespace Alumni.Services.ChatService
{
    public interface IMessageService
    {
        Task<ServiceResponse<MessageReadDTO>> SendMessageAsync(MessageCreateDTO messageCreateDTO, Guid senderId);

        // Mark a message as read by the receiver and to show status of the message as read in the chat history
        Task<ServiceResponse<MessageReadDTO>> ReadMessageAsync(Guid messageId, Guid userId);
        Task<ServiceResponse<IEnumerable<MessageReadDTO>>> GetChatHistoryAsync(Guid conversationId, int limit = 50);
    }
}

