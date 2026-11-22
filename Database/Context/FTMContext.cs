
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public class FTMContext : DbContext
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    @"Host=localhost;Port=5432;Database=FTM;Username=postgres;Password=12345;",
                    npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()
                );
            }
        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<TransferDetails> TransferDetails { get; set; }
        public DbSet<Transfer> Transfer { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerAgent> PlayerAgents { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<ClubPlayer> ClubPlayers { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<AgentInfo> AgentInfo { get; set; }
        public DbSet<Chat> Chat { get; set; } = null!;
        public DbSet<ChatMessage> ChatMessage { get; set; } = null!;
    }
}