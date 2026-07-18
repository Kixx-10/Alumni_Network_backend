namespace Alumni.DTOS
{
    public class FriendRequestResponseDTO
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string SenderAvatarUrl { get; set; } = string.Empty;
        public Guid ReceiverId { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedDate { get; set; }
    }
}
