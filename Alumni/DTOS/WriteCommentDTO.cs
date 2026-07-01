using System.ComponentModel.DataAnnotations;
namespace Alumni.Models.DTOs;

public class WriteCommentDTO
{
    [Required(ErrorMessage = "PostId is required.")]
    public Guid PostId { get; set; }

    [Required(ErrorMessage = "Comment content cannot be empty.")]
    public string Content { get; set; } = string.Empty;
    public Guid? ParentCommentId { get; set; }
}
