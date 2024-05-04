using Microsoft.EntityFrameworkCore;
using HackerNewsCloneApi.Models;

namespace HackerNewsCloneApi.Data
{
    public class HackerNewsContext : DbContext
    {
        public HackerNewsContext(DbContextOptions<HackerNewsContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships if necessary
        }
    }
}
