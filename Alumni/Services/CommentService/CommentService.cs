using Alumni.DTOS;
using Alumni.DTOS.Common;
using Alumni.Models.DTOs;
using Alumni.Models.Feeds;
using Alumni.Repository.CommentRepository;

namespace Alumni.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;

        public CommentService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }
        public async Task<ServiceResponse<ReadCommentDTO>> CreateCommentAsync(WriteCommentDTO dto, Guid userId)
        {
            var response = new ServiceResponse<ReadCommentDTO>();
            try
            {
                //Reply ဖြစ်ပါက Parent Comment ၏ တည်ရှိမှု စစ်ဆေး
                if (dto.ParentCommentId.HasValue)
                {
                    var parentComment = await _commentRepo.GetCommentByIdAsync(dto.ParentCommentId.Value);
                    if (parentComment == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Parent comment not found. Cannot create reply.";
                        return response;
                    }

                    //  Business rule — Parent comment သည် တခြား post ၏
                    //    comment မဟုတ်ကြောင်း သေချာစေသည် (data integrity)
                    if (parentComment.PostId != dto.PostId)
                    {
                        response.IsSuccess = false;
                        response.Message = "Parent comment does not belong to the specified post.";
                        return response;
                    }
                }

                var newComment = new Comment
                {
                    CommentId = Guid.NewGuid(),
                    PostId = dto.PostId,
                    UserId = userId,
                    Content = dto.Content,
                    ParentCommentId = dto.ParentCommentId,
                    CreatedDate = DateTime.UtcNow,
                };

                var result = await _commentRepo.AddCommentAsync(newComment);
                if (result <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to save comment.";
                    return response;
                }
                var savedComment = await _commentRepo.GetCommentByIdAsync(newComment.CommentId);

                response.Data = MapToReadDto(savedComment ?? newComment);
                response.IsSuccess = true;
                response.Message = "Comment created successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
        public async Task<ServiceResponse<List<ReadCommentDTO>>> GetPostCommentsAsync(Guid postId)
        {
            var response = new ServiceResponse<List<ReadCommentDTO>>();
            try
            {
                var allComments = await _commentRepo.GetCommentsByPostIdAsync(postId);

                if (allComments.Count == 0)
                {
                    response.Data = new List<ReadCommentDTO>();
                    response.IsSuccess = true;
                    response.Message = "No comments yet.";
                    return response;
                }

                response.Data = BuildCommentTree(allComments);
                response.IsSuccess = true;
                response.Message = "Comments fetched successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        private List<ReadCommentDTO> BuildCommentTree(List<Comment> allComments)
        {
            // ── အဆင့် 1: Comment တိုင်းကို DTO အဖြစ် ပြောင်း၍ Dictionary တွင် သိမ်း ──
            var dtoLookup = allComments.ToDictionary(
                c => c.CommentId,
                c => MapToReadDto(c)
            );

            var rootComments = new List<ReadCommentDTO>();

            // ── အဆင့် 2: Parent-Child ဆက်စပ်မှုကို ချိတ်ဆက်သည် ──
            foreach (var comment in allComments)
            {
                var dto = dtoLookup[comment.CommentId];

                if (comment.ParentCommentId.HasValue &&
                    dtoLookup.TryGetValue(comment.ParentCommentId.Value, out var parentDto))
                {
                    // Reply ဖြစ်လျှင် — Parent ၏ Replies list ထဲ ထည့်
                    parentDto.Replies.Add(dto);
                }
                else
                {
                    // Root comment ဖြစ်လျှင် — အဓိက list ထဲ ထည့်
                    rootComments.Add(dto);
                }
            }

            return rootComments.OrderBy(c => c.CreatedDate).ToList();
        }
        private ReadCommentDTO MapToReadDto(Comment comment)
        {
            return new ReadCommentDTO
            {
                CommentId = comment.CommentId,
                PostId = comment.PostId,
                Content = comment.Content,
                CreatedDate = comment.CreatedDate,
                UpdatedDate = comment.UpdatedDate,
                UserId = comment.UserId,
                UserName = comment.User?.Name ?? "Unknown User",
                UserProfileImage = comment.User?.Profile?.AvatarUrl,
                ParentCommentId = comment.ParentCommentId,
                Replies = new List<ReadCommentDTO>(),
            };
        }
    }
}
