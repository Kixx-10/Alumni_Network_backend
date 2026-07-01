namespace Alumni.Models.DTOs
{
    public class ResponseProfileDTO
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string? Class { get; set; }
        public string? AvatarUrl { get; set; }
        public int? GraduationYear { get; set; }
        public string? Department { get; set; }
        public string? University { get; set; }
        public string? Company { get; set; }
        public string? JobTitle { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}