using System;
using System.Collections.Generic;
using System.Text;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Database.Context
{
    public class FTMContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Host=localhost;Port=5432;Database=IMS;Username=postgres;Password=12345;",
                npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
        }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<TransferDetails> TransferDetails { get; set; }
        public DbSet<Transfer> Transfer { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<PlayerBook> PlayerBook { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Payment> Paymernt { get; set; }
        public DbSet<BaseModel> BaseModel { get; set; }
        public DbSet <AgentInfo> AgentInfo { get; set; }
    } 
}
