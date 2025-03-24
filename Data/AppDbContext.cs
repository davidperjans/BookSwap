using Microsoft.EntityFrameworkCore;
using BookSwap.Models;

namespace BookSwap.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<TradeRequest> TradeRequests { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=bookswapdb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 📌 User -> Book (En användare kan ha flera böcker)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Books)
                .WithOne(b => b.Owner)
                .HasForeignKey(b => b.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // 📌 User -> FriendRequests (Skickade)
            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Sender)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(fr => fr.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // 📌 User -> FriendRequests (Mottagna)
            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Receiver)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(fr => fr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // 📌 Friendship (Relation mellan två users)
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User1)
                .WithMany()
                .HasForeignKey(f => f.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User2)
                .WithMany()
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // 📌 User -> TradeRequests (Skickade)
            modelBuilder.Entity<TradeRequest>()
                .HasOne(tr => tr.Sender)
                .WithMany(u => u.SentTradeRequests)
                .HasForeignKey(tr => tr.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // 📌 User -> TradeRequests (Mottagna)
            modelBuilder.Entity<TradeRequest>()
                .HasOne(tr => tr.Receiver)
                .WithMany(u => u.ReceivedTradeRequests)
                .HasForeignKey(tr => tr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
