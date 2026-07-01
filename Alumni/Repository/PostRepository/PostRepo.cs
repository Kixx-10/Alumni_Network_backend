using Alumni.Data;
using Alumni.Models.Feeds;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.PostRepository
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDbContext _context;
        public PostRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Post> CreatePostRepository(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<IEnumerable<Post>> GetAllPostRepository()
        {
            return await _context.Posts
                 .Include(p => p.Author)
                     .ThenInclude(a => a!.Profile)
                 .Include(p => p.Comments)
                 .Include(p => p.Likes)
                 .Include(p => p.Shares)
                 .OrderByDescending(p => p.CreatedDate)
                 .ToListAsync();
        }
    }
}
