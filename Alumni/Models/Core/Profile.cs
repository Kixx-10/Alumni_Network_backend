using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumni.Models.Core
{
    [Table("Profiles")]
    public class Profile
    {
        [Key]
        public Guid ProfileId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Class { get; set; }

        [MaxLength(100)]
        public string? AvatarUrl { get; set; }

        public int? GraduationYear { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        [MaxLength(200)]
        public string? University { get; set; }

        [MaxLength(100)]
        public string? Company { get; set; }

        [MaxLength(100)]
        public string? JobTitle { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }
    }
}