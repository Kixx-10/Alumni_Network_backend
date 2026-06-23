using Alumni.Data;
using Alumni.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Repository.SignUpRepository
{
    public class SignUpRepo : ISignUpRepo
    {
        private readonly AppDbContext _context;

        public SignUpRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailExistedAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<int> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync();
        }
    }
}