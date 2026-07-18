using Alumni.Data;
using Alumni.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.ChatRepository
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly AppDbContext _context;

        public ConversationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Conversation?> GetConversationBetweenUsersAsync(Guid user1Id, Guid user2Id)
        {
            return await _context.Conversations

                .Include(c => c.LastMessage)
                .Where(c => (c.User1Id == user1Id && c.User2Id == user2Id)
                         || (c.User1Id == user2Id && c.User2Id == user1Id))
                .FirstOrDefaultAsync();
        }
        public async Task<Conversation> CreateConversationAsync(Conversation conversation)
        {
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            return conversation;
        }

        public async Task<IEnumerable<Conversation>> GetUserConversationsAsync(Guid userId)
        {
            return await _context.Conversations
                .AsNoTracking()
                .Include(c => c.User1)
                    .ThenInclude(u => u.Profile)
                .Include(c => c.User2)
                    .ThenInclude(u => u.Profile)
                .Include(c => c.LastMessage)
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .OrderByDescending(c => c.UpdatedDate ?? c.CreatedDate)
                .ToListAsync();
        }

        public async Task UpdateConversationLastMessageAsync(Guid conversationId, Guid lastMessageId)
        {
            var conversation = await _context.Conversations.FindAsync(conversationId);

            if (conversation != null)
            {
                conversation.LastMessageId = lastMessageId;
                conversation.UpdatedDate = DateTime.UtcNow;

                _context.Conversations.Update(conversation);
                await _context.SaveChangesAsync();
            }
        }
    }
}