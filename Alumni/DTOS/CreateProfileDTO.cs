namespace Alumni.Models.DTOs
{
    public class CreateProfileDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string? Class { get; set; }
        public string? AvatarUrl { get; set; }
        public int? GraduationYear { get; set; }
        public string? Department { get; set; }
        public string? University { get; set; }
        public string? Company { get; set; }
        public string? JobTitle { get; set; }
    }
}