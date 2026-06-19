using Alumni.Models.Core;

namespace Alumni.Repository.SignUpRepository
{
    public interface ISignUpRepo
    {

        Task<bool> IsEmailExistedAsync(string email);


        Task<int> AddUserAsync(User user);
    }
}