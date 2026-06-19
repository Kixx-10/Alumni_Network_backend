using Alumni.Models.Core;

namespace Alumni.Services.TokenService
{
    public interface ITokenService
    {
        String CreateToken(User user);
    }
}
