namespace Alumni.DTOS
{
    public class ReadCommentDTO
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? UserProfileImage { get; set; }
        public Guid? ParentCommentId { get; set; }
        public List<ReadCommentDTO> Replies { get; set; } = new List<ReadCommentDTO>();
    }
}
