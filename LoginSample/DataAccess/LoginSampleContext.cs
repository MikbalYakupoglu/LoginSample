using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LoginSampleContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-BCSUV51;Database=LoginSample;Trusted_Connection=True;TrustServerCertificate=True");
        }


        public DbSet<User> Users { get; set; }
    }
}
