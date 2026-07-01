namespace Alumni.DTOs
{
    public class MessageReadDTO
    {
        public Guid MessageId { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string MessageStatus { get; set; } = "Sent"; // default
        public string? AttachmentUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
