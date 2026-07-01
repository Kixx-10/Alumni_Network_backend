namespace Alumni.DTOS
{
    public class PostResponseDTO
    {
        public Guid PostId { get; set; }
        public string? Content { get; set; }
        public string? MediaUrls { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int ShareCount { get; set; }
        public DateTime CreatedDate { get; set; }

        // To show name of author
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorAvatarUrl { get; set; } = string.Empty;
    }
}
