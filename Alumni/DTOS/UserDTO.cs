using Alumni.Models.Core;

namespace Alumni.DTOS
{
    public class UserDTO
    {
        public String Name { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public String Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
