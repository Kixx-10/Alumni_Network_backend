using Alumni.Data;
using Alumni.Models.Feeds;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.ActionRepository
{
    public class LikeRepo : ILikeRepo
    {
        private readonly AppDbContext _context;

        public LikeRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Like?> GetLlikeAsync(Guid postId, Guid userId)
        {
            return await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
        }

        public async Task AddLikeAsync(Like like)
        {
            await _context.Likes.AddAsync(like);
        }

        public void RemoveLike(Like like)
        {
            _context.Likes.Remove(like);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }
    }
}