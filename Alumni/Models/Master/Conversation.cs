using Alumni.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumni.Models.Master
{
    public class Conversation
    {
        [Key]
        public Guid ConversationId { get; set; } = Guid.NewGuid();

        // Participant 1 
        [Required]
        public Guid User1Id { get; set; }

        [ForeignKey("User1Id")]
        public virtual User User1 { get; set; } = null!;

        // Participant 2 
        [Required]
        public Guid User2Id { get; set; }

        [ForeignKey("User2Id")]
        public virtual User User2 { get; set; } = null!;

        public Guid? LastMessageId { get; set; }

        [ForeignKey("LastMessageId")]
        public virtual Message? LastMessage { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Messages collection 
        public virtual ICollection<Message> Messages { get; set; }
            = new List<Message>();
    }
}
