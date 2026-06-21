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
            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<IEnumerable<Post>> GetAllPostRepository()
        {
            return await _context.Post
                 .Include(p => p.Author)
                 .OrderByDescending(p => p.CreatedDate) // latest post order date
                 .ToListAsync();
        }
    }
}
