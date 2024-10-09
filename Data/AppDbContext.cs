using Microsoft.EntityFrameworkCore;
using MinhaLoja.Models;

namespace MinhaLoja.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("DataSource=app.db; Cache=Shared");
        }
    }
}