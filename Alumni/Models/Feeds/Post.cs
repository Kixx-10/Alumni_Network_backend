using Alumni.Models.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumni.Models.Feeds
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User? Author { get; set; }
        public string? Content { get; set; }

        public string? MediaUrls { get; set; }

        public int LikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int ShareCount { get; set; } = 0;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        public DateTime? UpdatedDate { get; set; }

    }
}
