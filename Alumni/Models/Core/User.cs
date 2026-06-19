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
        public DateTime CreaatedDate = DateTime.UtcNow.Date;

        public DateTime? UpdatedDate { get; set; }

    }
}
