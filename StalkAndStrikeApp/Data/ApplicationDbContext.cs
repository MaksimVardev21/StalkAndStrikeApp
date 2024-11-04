using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StalkAndStrikeApp.Models;

namespace StalkAndStrikeApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection here
            optionsBuilder.UseSqlServer("YourConnectionStringHere");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Hunt>().HasKey(h => h.HuntId);

            // Configure relationships (if any) and other constraints
            modelBuilder.Entity<Hunt>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure string properties with length constraints (optional)
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Hunt>()
                .Property(h => h.Location)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Hunt>()
                .Property(h => h.GameTracked)
                .HasMaxLength(50)
                .IsRequired();
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Hunt> Hunt { get; set; }

    } 
}
