namespace Alumni.Services.UserService
{
    public interface IUserService
    {
        Task<(bool IsOnline, DateTime? LastSeen)> GetUserStatusAsync(Guid userId);
        Task SetUserOnlineAsync(Guid userId, bool isOnline, DateTime? lastSeen = null);
    }
}
