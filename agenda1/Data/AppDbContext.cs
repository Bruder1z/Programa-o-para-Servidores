using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Contato> Contatos {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder opt) 
            => opt.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}