using Alumni.Data;
using Alumni.Models.Feeds;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.CommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            return await _context.SaveChangesAsync();
        }
        public async Task<Comment?> GetCommentByIdAsync(Guid commentId)
        {
            return await _context.Comments
                .SingleOrDefaultAsync(c => c.CommentId == commentId);
        }
        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            var allComments = await _context.Comments
                .AsNoTracking()
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                    .ThenInclude(u => u!.Profile)
                .OrderBy(c => c.CreatedDate)
                .ToListAsync();

            return allComments;
        }
    }
}
