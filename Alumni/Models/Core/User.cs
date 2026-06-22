using Alumni.Models.Feeds;
using System.ComponentModel.DataAnnotations;

namespace Alumni.Models.Core
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public String Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public String Email { get; set; } = string.Empty;

        public String Password { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Share> Shares { get; set; } = new List<Share>();

    }
}
