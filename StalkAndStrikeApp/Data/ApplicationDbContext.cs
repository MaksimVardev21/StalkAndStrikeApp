using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StalkAndStrikeApp.Models;

namespace StalkAndStrikeApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection here
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
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

            // Configure the relationships between Hunter, Dog, and Squad

            // Hunter to Squad: Many Hunters in one Squad
            modelBuilder.Entity<Hunter>()
                .HasOne(h => h.Squad)
                .WithMany(s => s.Hunters)
                .HasForeignKey(h => h.SquadId)
                .OnDelete(DeleteBehavior.Cascade);

            // Dog to Hunter: One Dog per Hunter
            modelBuilder.Entity<Dog>()
                .HasOne(d => d.Hunter)
                .WithMany(h => h.Dogs) // Assuming a hunter could have multiple dogs
                .HasForeignKey(d => d.HunterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure unique constraints or other specifications as needed
            modelBuilder.Entity<Hunter>()
                .HasIndex(h => h.LicenseNumber)
                .IsUnique(); // Ensuring each hunter has a unique license number

            modelBuilder.Entity<Dog>()
                .HasIndex(d => d.Name)
                .IsUnique(); // Ensure unique names for dogs in this example
        }

        public DbSet<User> Users { get; set; }
        public DbSet<HuntingLocation> HuntingLocations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Trophy> Trophies { get; set; }

        public DbSet<Hunter> Hunters { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Squad> Squads { get; set; }

    }

        

        
    
} 
