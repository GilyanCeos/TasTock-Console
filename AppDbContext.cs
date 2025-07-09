using Microsoft.EntityFrameworkCore;
using TasTock.Models;

namespace TasTock
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Itens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=tasTock.db");
        }
    }
}
