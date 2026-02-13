using System.ComponentModel.DataAnnotations;

namespace cse325_team1.Data.Models;

public class UserProfile
{
    public int Id { get; set; }

    [Required, MaxLength(60)]
    public string Username { get; set; } = "";

    // NOTE: store a HASH here, not the raw password
    [Required, MaxLength(255)]
    public string PasswordHash { get; set; } = "";

    [Required, MaxLength(60)]
    public string FirstName { get; set; } = "";

    [Required, MaxLength(60)]
    public string LastName { get; set; } = "";

    public List<JournalEntry> JournalEntries { get; set; } = new();
    public List<Event> Events { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
}
