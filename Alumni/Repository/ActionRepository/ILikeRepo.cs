using Alumni.Models.Feeds;

namespace Alumni.Repository.ActionRepository
{
    public interface ILikeRepo
    {
        Task<Like?> GetLlikeAsync(Guid postId, Guid userId);
        Task AddLikeAsync(Like like);
        void RemoveLike(Like like);
        Task<bool> SaveChangesAsync();
    }
}
