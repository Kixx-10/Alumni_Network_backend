using Alumni.DTOS;
using Alumni.DTOS.Common;
using Alumni.Models.Core;

namespace Alumni.Services.PostServices
{
    public interface IPostService
    {
        Task<ServiceResponse<PostResponseDTO>> CreatePostService(CreatePostDTO createPostDto, Guid userId, UserRole userRole);
        Task<ServiceResponse<IEnumerable<PostResponseDTO>>> GetAllPostService();
    }
}
