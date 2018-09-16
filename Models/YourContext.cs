using Microsoft.EntityFrameworkCore;

namespace CleverIdeas.Models
{
    public class YourContext : DbContext
    {
        public YourContext(DbContextOptions<YourContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}