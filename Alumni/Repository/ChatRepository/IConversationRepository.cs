using Alumni.Models.Master;
namespace Alumni.Repository.ChatRepository
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetConversationBetweenUsersAsync(Guid user1Id, Guid user2Id);

        Task<Conversation> CreateConversationAsync(Conversation conversation);

        Task UpdateConversationLastMessageAsync(Guid conversationId, Guid lastMessageId);
    }
}

