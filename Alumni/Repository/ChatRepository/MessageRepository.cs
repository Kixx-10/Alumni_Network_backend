using Alumni.Data;
using Alumni.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.ChatRepository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;
        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<Message?> ReadMessageAsync(Guid messageId)
        {
            return await _context.Messages.FindAsync(messageId);
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, int limit = 50)
        {
            var latestMessages = await _context.Messages
        .AsNoTracking()
        .Where(m => m.ConversationId == conversationId)
        .OrderByDescending(m => m.CreatedDate)
        .Take(limit)
        .ToListAsync();
            return latestMessages.OrderBy(m => m.CreatedDate);
        }
    }
}
