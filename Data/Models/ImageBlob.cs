using System.ComponentModel.DataAnnotations;

namespace cse325_team1.Data.Models;

// named ImageBlob to avoid conflicts with System.Drawing.Image etc.
public class ImageBlob
{
    public int Id { get; set; }

    // Optional FK -> Event
    public int? EventId { get; set; }
    public Event? Event { get; set; }

    // Optional FK -> JournalEntry
    public int? JournalId { get; set; }
    public JournalEntry? Journal { get; set; }

    // The actual bytes
    [Required]
    public byte[] Blob { get; set; } = Array.Empty<byte>();

    // Optional helpful metadata (recommended)
    public string? ContentType { get; set; } // "image/png"
    public string? FileName { get; set; }    // "photo1.png"

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
