namespace Alumni.DTOS
{
    public class UserDiscoverDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
