using Alumni.DTOS;
using Alumni.DTOS.Common;
using Alumni.Models.DTOs;

namespace Alumni.Services.CommentService
{
    public interface ICommentService
    {
        Task<ServiceResponse<ReadCommentDTO>> CreateCommentAsync(WriteCommentDTO dto, Guid userId);

        Task<ServiceResponse<List<ReadCommentDTO>>> GetPostCommentsAsync(Guid postId);
    }
}
