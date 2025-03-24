using Microsoft.EntityFrameworkCore;
using BookSwap.Models;

namespace BookSwap.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<TradeRequest> TradeRequests { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=bookswapdb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
