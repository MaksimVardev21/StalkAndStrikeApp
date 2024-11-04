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
        public DbSet<User> Users { get; set; }
        public DbSet<Hunt> Hunt { get; set; }
    } 
}
