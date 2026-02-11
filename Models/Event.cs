using System.ComponentModel.DataAnnotations;

namespace cse325_team1.Models;

public class Event
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Title { get; set; } = "";

    public DateTime DateTime { get; set; }

    public string? Description { get; set; }

    [MaxLength(80)]
    public string? Category { get; set; }

    [MaxLength(140)]
    public string? Location { get; set; }

    // FK -> UserProfile
    public int UserId { get; set; }
    public UserProfile? User { get; set; }

    public List<ImageBlob> Images { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
