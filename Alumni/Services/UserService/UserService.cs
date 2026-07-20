using Alumni.Data;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool IsOnline, DateTime? LastSeen)> GetUserStatusAsync(Guid userId)
        {
            var user = await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new { u.IsOnline, u.LastSeen })
                .FirstOrDefaultAsync();

            return user == null ? (false, null) : (user.IsOnline, user.LastSeen);
        }

        public async Task SetUserOnlineAsync(Guid userId, bool isOnline, DateTime? lastSeen = null)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            user.IsOnline = isOnline;
            if (lastSeen.HasValue)
            {
                user.LastSeen = lastSeen.Value;
            }

            await _context.SaveChangesAsync();
        }
    }
}