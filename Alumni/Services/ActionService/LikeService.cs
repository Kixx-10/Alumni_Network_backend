using Alumni.Data;
using Alumni.DTOS;
using Alumni.Models.Feeds;
using Alumni.Repository.ActionRepository;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Services.ActionService
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepo _likeRepo;
        private readonly AppDbContext _context; // to directily change database

        public LikeService(ILikeRepo likeRepo, AppDbContext context)
        {
            _likeRepo = likeRepo;
            _context = context;
        }

        public async Task<string> ToggleLikeAsync(LikeDTO likeDto)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == likeDto.PostId);
            if (post == null) return "Post Not Found";

            // check user liked 
            var existingLike = await _likeRepo.GetLlikeAsync(likeDto.PostId, likeDto.UserId);

            if (existingLike != null)
            {
                _likeRepo.RemoveLike(existingLike);

                if (post.LikeCount > 0) post.LikeCount--;
                await _likeRepo.SaveChangesAsync();
                return "Unliked";
            }
            else
            {
                var newLike = new Like
                {
                    PostId = likeDto.PostId,
                    UserId = likeDto.UserId,
                    LikedDate = DateTime.UtcNow.Date
                };

                await _likeRepo.AddLikeAsync(newLike);
                post.LikeCount++;
                //real store in database
                await _likeRepo.SaveChangesAsync();
                return "Liked";
            }
        }
    }
}