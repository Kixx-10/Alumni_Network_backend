using Alumni.Models.Feeds;

namespace Alumni.Repository.CommentRepository
{
    public interface ICommentRepository
    {
        Task<int> AddCommentAsync(Comment comment);

        Task<Comment?> GetCommentByIdAsync(Guid commentId);
        Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId);
    }
}
