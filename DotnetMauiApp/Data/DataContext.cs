using DotnetMauiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetMauiApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            
        }

        public DbSet<Transaction> Transactions{ get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var db = Path.Combine(FileSystem.AppDataDirectory, "sqlite3.db");
            optionsBuilder.UseSqlite($"Filename = {db}"); 
        }
    }
}
