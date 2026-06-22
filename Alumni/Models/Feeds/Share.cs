using Alumni.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumni.Models.Feeds
{
    public class Share
    {
        [Key]
        public Guid ShareId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post? Post { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required]
        [StringLength(50)]
        public string SharePlatform { get; set; } = "Internal";

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;
    }
}