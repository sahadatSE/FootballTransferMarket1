using System.Collections.Generic;
using System.Data;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public class FootballTransferMarketContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-POOH2AD\SQLEXPRESS;Database=FootballTransferMarket;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0");
            optionsBuilder.UseSqlServer(@"Server=.;Database=FootballTransferMarket;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0");
        }

        public DbSet<User> User { get; set; }
        public DbSet<BaseModel> BaseModel { get; set; }
        public DbSet<OfferForSale> OfferForSale { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerBook> PlayerBook { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Transfer> Transfer { get; set; }
       



    }
}