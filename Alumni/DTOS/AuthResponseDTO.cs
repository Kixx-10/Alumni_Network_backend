namespace Alumni.DTOS
{
    public class AuthResponseDTO
    {
        public UserDTO User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
    }
}
