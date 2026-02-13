using Microsoft.EntityFrameworkCore;
using cse325_team1.Data.Models;

namespace cse325_team1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<JournalEntry> JournalEntries { get; set; }

        public DbSet<UserProfile> Users { get; set; }
        
    }
}