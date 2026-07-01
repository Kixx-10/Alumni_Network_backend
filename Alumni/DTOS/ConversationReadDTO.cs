namespace Alumni.DTOs
{
    public class ConversationReadDTO
    {
        public Guid ConversationId { get; set; }
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }
        public Guid? LastMessageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Optional: include last message preview
        public MessageReadDTO? LastMessage { get; set; }
    }
}
