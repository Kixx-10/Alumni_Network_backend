using Alumni.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumni.Models.Master
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; } = Guid.NewGuid();

        // Conversation FK 
        [Required]
        public Guid ConversationId { get; set; }

        [ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; } = null!;

        // Sender FK 
        [Required]
        public Guid SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; } = null!;

        // ── Receiver FK 
        [Required]
        public Guid ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; } = null!;

        // ── Content 
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
