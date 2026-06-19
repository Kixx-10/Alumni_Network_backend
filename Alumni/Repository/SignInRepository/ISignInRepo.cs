using Alumni.Models.Core;

namespace Alumni.Repository.SignInRepository
{
    public interface ISignInRepo
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}