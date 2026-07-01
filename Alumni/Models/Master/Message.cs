using Alumni.Models.Core;
using System.ComponentModel.DataAnnotations;

namespace Alumni.Models.Master
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }

        [Required]
        public Guid SenderId { get; set; }
        public virtual User Sender { get; set; }

        [Required]
        public Guid ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string MessageStatus { get; set; } = "Sent";

        public string? AttachmentUrl { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }
    }
}
