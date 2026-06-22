using Alumni.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumni.Models.Feeds
{
    public class Like
    {
        [Key]
        [Required]
        public Guid LikeId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post? Post { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required]
        public DateTime LikedDate { get; set; } = DateTime.UtcNow.Date;
    }
}
