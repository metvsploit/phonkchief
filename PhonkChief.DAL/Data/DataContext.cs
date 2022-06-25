using Microsoft.EntityFrameworkCore;
using PhonkChief.Domain.Entities;

namespace PhonkChief.DAL.Data
{
    public class DataContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Loop> Loops { get; set; }

        public DataContext()
        {
 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=phonkchiefdb;Trusted_Connection=True;");
        }
    }
}
