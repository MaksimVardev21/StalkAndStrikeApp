using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StalkAndStrikeApp.Models;

namespace StalkAndStrikeApp.Data
{
    public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Hunter> Hunters { get; set; }
    public DbSet<Squad> Squads { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<HuntingLocation> HuntingLocations { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Trophy> Trophies { get; set; }
    public DbSet<Gun> Gun { get; set; }
    public DbSet<Category> Category { get; set; }
        public DbSet<StalkAndStrikeApp.Models.HuntedPlace> HuntedPlace { get; set; } = default!;
}
}