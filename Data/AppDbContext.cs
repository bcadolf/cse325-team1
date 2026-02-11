using Microsoft.EntityFrameworkCore;
using cse325_team1.Models;

namespace cse325_team1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<JournalEntry> JournalEntries { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
        
    }
}