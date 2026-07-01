using Alumni.Models.Core;
using System.ComponentModel.DataAnnotations;

namespace Alumni.Models.Master
{
    public class Conversation
    {
        [Key]
        public Guid ConversationId { get; set; } = Guid.NewGuid();

        // Participants
        [Required]
        public Guid User1Id { get; set; }
        public virtual User User1 { get; set; }

        [Required]
        public Guid User2Id { get; set; }
        public virtual User User2 { get; set; }

        // Last message reference (One-to-One)
        public Guid? LastMessageId { get; set; }
        public virtual Message? LastMessage { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation property (One-to-Many)
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
