using Alumni.Data;
using Alumni.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.SignInRepository
{
    public class SignInRepo : ISignInRepo
    {
        private readonly AppDbContext _context;

        public SignInRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}