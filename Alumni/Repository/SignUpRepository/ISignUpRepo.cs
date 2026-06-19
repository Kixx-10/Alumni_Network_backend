using Alumni.Models.Core;

namespace Alumni.Repository.SignUpRepository
{
    public interface ISignUpRepo
    {
        // Email အကောင့် ရှိပြီးသားလား စစ်ဆေးရန်
        Task<bool> IsEmailExistedAsync(string email);

        // အသုံးပြုသူအသစ်အား Database ထဲသို့ ထည့်သွင်းသိမ်းဆည်းရန်
        Task<int> AddUserAsync(User user);
    }
}