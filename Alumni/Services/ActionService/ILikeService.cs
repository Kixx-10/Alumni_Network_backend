using Alumni.DTOS;

namespace Alumni.Services.ActionService
{
    public interface ILikeService
    {
        Task<string> ToggleLikeAsync(LikeDTO likeDto);
    }
}
