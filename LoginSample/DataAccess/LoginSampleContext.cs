using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DataAccess
{
    public class LoginSampleContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);
            modelBuilder.Entity<UserRole>().HasQueryFilter(ur => ur.User.IsActive);
            modelBuilder.Entity<Article>().HasQueryFilter(a => a.Creator.IsActive);

            modelBuilder.Entity<Article>().HasQueryFilter(a => !a.IsDeleted);


            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);


            modelBuilder.Entity<Article>().HasIndex(a => a.CreatedAt);
            modelBuilder.Entity<Category>().HasIndex(c => c.Name);
            modelBuilder.Entity<Role>().HasIndex(r => r.Name);
            modelBuilder.Entity<User>().HasIndex(u => u.Email);
            modelBuilder.Entity<UserRole>().HasIndex(ur => ur.UserId);




            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-BCSUV51;Database=LoginSample;Trusted_Connection=True;TrustServerCertificate=True");
            //.LogTo(Console.WriteLine, LogLevel.Trace);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Article?> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
