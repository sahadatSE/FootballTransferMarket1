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
       
    }
}