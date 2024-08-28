using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Contato> Contatos {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}