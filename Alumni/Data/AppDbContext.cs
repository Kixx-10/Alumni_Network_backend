using Alumni.Models.Core;
using Alumni.Models.Feeds;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
    }
}
