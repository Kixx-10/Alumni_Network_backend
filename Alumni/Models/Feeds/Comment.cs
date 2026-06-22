using Alumni.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumni.Models.Feeds
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post? Post { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public Guid? ParentCommentId { get; set; }

        [ForeignKey("ParentCommentId")]
        public virtual Comment? ParentComment { get; set; }

        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        public DateTime? UpdatedDate { get; set; }
    }
}