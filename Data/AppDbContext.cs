using Microsoft.EntityFrameworkCore;
using CSE325Team1.Models;

namespace CSE325Team1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<JournalEntry> JournalEntries { get; set; }
    }
}