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
            base.OnModelCreating(modelBuilder);

            // Configure primary keys
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<HuntingLocation>().HasKey(h => h.Id);
            modelBuilder.Entity<Schedule>().HasKey(s => s.Id);
            modelBuilder.Entity<Trophy>().HasKey(t => t.Id);

            // Configure relationships
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Location)
                .WithMany()
                .HasForeignKey(s => s.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure User entity properties
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

            // Configure HuntingLocation entity properties
            modelBuilder.Entity<HuntingLocation>()
                .Property(h => h.Name)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<HuntingLocation>()
                .Property(h => h.AllowedGame)
                .HasMaxLength(50)
                .IsRequired();

            // Configure Trophy entity properties
            modelBuilder.Entity<Trophy>()
                .Property(t => t.Description)
                .HasMaxLength(500)
                .IsRequired();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<HuntingLocation> HuntingLocations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Trophy> Trophies { get; set; }
    }
} 
