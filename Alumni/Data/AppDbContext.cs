using Alumni.Models.Core;
using Alumni.Models.Feeds;
using Alumni.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace Alumni.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Conversation> Converataions { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Profile → User (One-to-One)
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Conversation → LastMessage (One-to-One)
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.LastMessage)
                .WithOne()
                .HasForeignKey<Conversation>(c => c.LastMessageId)
                .OnDelete(DeleteBehavior.Restrict);

            // Conversation → Messages (One-to-Many)
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
