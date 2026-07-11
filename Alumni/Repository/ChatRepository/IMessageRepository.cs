using Alumni.Models.Master;

namespace Alumni.Repository.ChatRepository
{
    public interface IMessageRepository
    {
        Task<Message> CreateMessageAsync(Message message);
        Task<Message?> ReadMessageAsync(Guid messageId);
        Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, int limit = 50);
    }
}