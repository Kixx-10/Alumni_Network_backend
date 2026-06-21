using Alumni.Models.Feeds;

namespace Alumni.Repository.PostRepository
{
    public interface IPostRepo
    {
        Task<Post> CreatePostRepository(Post post);
        Task<IEnumerable<Post>> GetAllPostRepository();
    }
}
